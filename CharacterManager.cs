using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillBridge.Message;
using GameServer.Entities;

namespace GameServer.Managers
{
    class CharacterManager : Singleton<CharacterManager>
    {
        public Dictionary<int, Character> Characters = new Dictionary<int, Character>();

        public CharacterManager()
        {
        }

        public void Dispose()
        {
        }

        public void Init()
        {

        }

        public void Clear()
        {
            this.Characters.Clear();
        }

        public Character AddCharacter(TCharacter cha)
        {
            Character character = new Character(CharacterType.Player, cha);//客户端修改角色，服务端也要跟着改
            EntityManager.Instance.AddEntity(cha.MapID, character);
            this.Characters[character.Id] = character;
            return character;
        }


        public void RemoveCharacter(int characterId)
        {
            if (this.Characters.TryGetValue(characterId, out var cha))
            {
                EntityManager.Instance.RemoveEntity(cha.Data.MapID, cha);
                this.Characters.Remove(characterId);
            }
            else
            {
                Console.WriteLine($"Error: Character with ID {characterId} not found.");
            }
        }
    }
}
