using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Cronica.Core.ResourceManager
{
    public static class CronicaResourceManager
    {
        // 用于跟踪每帧的资源加载情况
        private static Dictionary<string, int> _loadCountThisFrame = new Dictionary<string, int>();
        private static Dictionary<string, int> _loadCountLastFrame = new Dictionary<string, int>();
        private static int _currentFrameCount;

        // 加载资源方法（带性能检测）
        public static T LoadAsset<T>(string path) where T : Object
        {
            // 记录当前帧的资源加载
            TrackFrameLoad(path);

            // 异步加载资源
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

            // 等待加载完成（注意：这会阻塞主线程）
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            else
            {
                Debug.LogError($"资源加载失败，路径: {path}");
                return null;
            }
        }

        // 跟踪每帧加载情况
        private static void TrackFrameLoad(string path)
        {
            // 检查是否是新的一帧
            if (Time.frameCount != _currentFrameCount)
            {
                // 新帧 - 转移上一帧的统计数据
                _loadCountLastFrame = new Dictionary<string, int>(_loadCountThisFrame);
                _loadCountThisFrame.Clear();
                _currentFrameCount = Time.frameCount;
            }

            // 记录当前加载
            if (_loadCountThisFrame.ContainsKey(path))
            {
                _loadCountThisFrame[path]++;
            }
            else
            {
                _loadCountThisFrame[path] = 1;
            }

            // 检查这个资源是否在上一帧也被加载了
            if (_loadCountLastFrame.TryGetValue(path, out int lastFrameCount) && lastFrameCount > 0)
            {
                Debug.LogError($"性能警告: 资源 '{path}' 正在被每帧加载！ " +
                               $"上一帧加载次数: {lastFrameCount}, 本帧已加载: {_loadCountThisFrame[path]}次。 " +
                               "建议缓存已加载的资源。");
            }
        }
    }
}