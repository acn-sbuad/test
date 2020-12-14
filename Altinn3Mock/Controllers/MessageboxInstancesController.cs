using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altinn.Platform.Storage.Interface.Models;
using Altinn3Mock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Altinn3Mock.Controllers
{
    [Route("storage/api/v1/sbl/instances")]
    [ApiController]
    public class MessageboxInstancesController : ControllerBase
    {
        /// <summary>
        /// Gets all instances in a given state for a given instance owner.
        /// </summary>
        /// <param name="instanceOwnerPartyId">the instance owner id</param>
        /// <param name="state">the instance state; active, archived or deleted</param>
        /// <param name="language"> language nb, en, nn-NO</param>
        /// <returns>list of instances</returns>
        [HttpGet("{instanceOwnerPartyId:int}")]
        public async Task<ActionResult> GetMessageBoxInstanceList(int instanceOwnerPartyId, [FromQuery] string state, [FromQuery] string language)
        {
            string[] allowedStates = { "active", "archived", "deleted" };
            string[] acceptedLanguages = { "en", "nb", "nn" };

            string languageId = "nb";
            if (string.IsNullOrEmpty(state))
            {
                return BadRequest($"State is empty. Please provide on of: {string.Join(", ", allowedStates)}");
            }

            state = state.ToLower();
            if (!allowedStates.Contains(state))
            {
                return BadRequest($"Invalid instance state. Please provide on of: {string.Join(", ", allowedStates)}");
            }

            if (language != null && acceptedLanguages.Contains(language.ToLower()))
            {
                languageId = language;
            }


            List<MessageBoxInstance> authorizedInstances = new List<MessageBoxInstance>
            {
                new MessageBoxInstance
                {
                    Id = "8cf3d156-0011-4d19-b154-c6ad7937e322",
                    InstanceOwnerId = instanceOwnerPartyId.ToString(),
                    Org = "skd",
                    AppName = "Sirius Mock Instance",
                    LastChangedBy = "50006836",
                    CreatedDateTime = DateTime.UtcNow,
                    ProcessCurrentTask = "FormFilling",
                    ReadStatus = ReadStatus.Read,           
                    Title = "Sirius Mock Instance"
                }
            };


            return Ok(authorizedInstances);
        }
    }
}
