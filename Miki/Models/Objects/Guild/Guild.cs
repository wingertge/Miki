using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Models.Objects.Guild
{
	[Table("Guild")]
	public class Guild
	{
		[Key, Column("id", Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long Id { get; set; }

		//public GuildUser User { get; set; }

		public ICollection<GuildRole> Roles { get; set; }
		//public ICollection<LocalExperience> LocalUsers { get; set; }
	}
}
