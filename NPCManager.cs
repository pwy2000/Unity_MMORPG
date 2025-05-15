using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers
{
    class NPCManager:Singleton<NPCManager>
    {
        public delegate bool NpcActionHandler(NPCDefine npc);
        Dictionary<NpcFunction, NpcActionHandler> eventMap = new Dictionary<NpcFunction, NpcActionHandler>();

        public void RegisterNpcEvent(NpcFunction function, NpcActionHandler action)
        {
            if (!eventMap.ContainsKey(function))
            {
                eventMap[function] = action;
            }
            else
                eventMap[function] += action;
        }
        public NPCDefine GetNPCDefine(int npcID)
        {
            NPCDefine npc = null;
            DataManager.Instance.NPCs.TryGetValue(npcID, out npc);
            return npc;
        }

        public bool Interactive(int npcId) 
        {
            if(DataManager.Instance.NPCs.ContainsKey(npcId))
            {
                var npc  = DataManager.Instance.NPCs[npcId];
                return Interactive(npc);
            }
            return false;
        }
        public bool Interactive(NPCDefine npc)
        {
            if (npc.Type == NpcType.Task)
            {
                return DoTaskInteractive(npc);//任务交互
            }
            else if (npc.Type == NpcType.Functional)
            {
                return DoFunctionIinteractive(npc);//功能交互
            }
            return false;
        }
        private bool DoTaskInteractive(NPCDefine npc)
        {
            MessageBox.Show("点击了NPC：" + npc.Name, "NPC对话");
            return true;
        }
        private bool DoFunctionIinteractive(NPCDefine npc)
        {
            MessageBox.Show("点击了NPC：" + npc.Name, "NPC对话");
           if (npc.Type != NpcType.Functional)
                return false;
           if(!eventMap.ContainsKey(npc.Function))
                return false;
           return eventMap[npc.Function](npc);
        }
    }
}
