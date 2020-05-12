using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My.VideosProject.Engines
{
    public class EngineContainerFactory
    {
        private static IEngine _container;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container"></param>
        public static void InitializeEngineContainerFactory(IEngine container)
        {
            _container = container;
        }
        /// <summary>
        /// 获取DI容器的实例
        /// </summary>
        /// <returns></returns>
        public static IEngine GetContainer()
        {
            return _container;
        }
    }
}