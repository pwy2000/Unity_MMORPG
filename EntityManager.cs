using Common;
using GameServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Managers
{
    class EntityManager : Singleton<EntityManager>
    {
        private int idx = 0;
        public List<Entity> AllEntities = new List<Entity> ();//用list类型维护所有的entity列表
        public Dictionary<int, List<Entity>> MapEntities = new Dictionary<int, List<Entity>> ();//用字典区分地图的entity

        public void AddEntity(int mapId, Entity entity)
        {
            AllEntities.Add(entity);
            //加入管理器生成唯一ID
            entity.EntityData.Id = ++this.idx;//总列表的索引作为ID

            List<Entity> entities = null;
            if (!MapEntities.TryGetValue(mapId, out entities))//如果列表不存在，创建一个新的
            {
                entities = new List<Entity>();
                MapEntities[mapId] = entities;
            }
            entities.Add(entity);
        }

        public void RemoveEntity(int mapId, Entity entity)
        {
            this.AllEntities.Remove(entity);
            this.MapEntities[mapId].Remove(entity);
        }
    }
}
