using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNPService.Shared.Models.Contracts;
using PNPService.Shared.Models.Contracts.Char;
using Newtonsoft.Json;
using PNPService.Shared.Models.Char;
using System.IO;

namespace PNPService.Shared.Controller
{
    public class CharController : ICharController
    {
        /// <summary>
        /// Loads a character from a json file stored in the users folder.
        /// </summary>
        /// <param name="characterName">The name of the character.</param>
        /// <param name="account">The account that owns the character.</param>
        /// <returns>The loaded character/null if it was not found.</returns>
        public ICharacter LoadCharacter(string characterName, IAccount account)
        {
            try
            {
                Character Char = JsonConvert.DeserializeObject<Character>(File.ReadAllText(@".\UserData\" + account.Name + @"\" + characterName + ".json"));

                return Char;
            }
            catch (Exception)
            {
                Console.WriteLine("Character: " + characterName + " was not found.");

                return null;
            }
        }


        /// <summary>
        /// Remove a character from an account.
        /// </summary>
        /// <param name="characterName">The name of the character to be removed.</param>
        /// <param name="account">The account that owns the character.</param>
        public void RemoveCharacter(string characterName, IAccount account)
        {
            try
            {
                File.Delete(@".\UserData\" + account.Name + @"\" + characterName +".json");
            }
            catch (Exception)
            {
                Console.WriteLine("Character: " + characterName + " was not found.");
            }
        }


        /// <summary>
        /// Save a character to a json file.
        /// </summary>
        /// <param name="character">The character object to be saved.</param>
        /// <param name="account">The account the character should take place on.</param>
        public void SaveCharacter(ICharacter character, IAccount account)
        {
            try
            {
                File.WriteAllText(@".\UserData\" + account.Name + @"\" + character.Name + ".json", JsonConvert.SerializeObject(character));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
