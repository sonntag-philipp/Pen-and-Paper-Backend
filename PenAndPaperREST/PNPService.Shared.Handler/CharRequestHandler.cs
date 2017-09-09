using PNPService.Shared.Controller;
using PNPService.Shared.Handler.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Handler
{
    public class CharRequestHandler : ICharRequestHandler
    {
        private MySQLController _MySQLController;

        public CharRequestHandler()
        {
            _MySQLController = new MySQLController(new ConfigController().Config);
        }

        public void SaveCharacter(string charContent, string charName)
        {
            _MySQLController.DoQuery(
                @"INSERT INTO `characters` (`identifier`, `character_name`, `character_content`) VALUES (NULL, @charName, @charContent)",
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("charName", charName),
                    new KeyValuePair<string, string>("charContent", charContent)
                }
            );
        }

        public string LoadCharacter(string userName, string charName)
        {
            string character = _MySQLController.DoQuery(
                @"SELECT `character_content` FROM `characters` WHERE `character_name` = @characterName AND `username` = @userName",
                new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("characterName", charName),
                    new KeyValuePair<string, string>("userName", userName)
                }
            );


            return character;
        }
    }
}
