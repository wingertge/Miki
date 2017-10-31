using IA.Events.Attributes;
using IA.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Modules.Roles
{
	[Module("Role management")]
	class RolesModule
	{
		[Command(Name = "iam")]
		public async Task IamAsync(EventContext e)
		{

		}
	}
}
