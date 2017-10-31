using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Models.Objects.Guild
{
	[Table("GuildRole")]
	public class GuildRole
	{
		[Key]
		[Column("id")]
		public long Id { get; set; }

		[Column("selectable"), DefaultValue(false)]
		public bool Selectable { get; set; }

		[Column("automatically_given"), DefaultValue(false)]
		public bool AutomaticallyGiven { get; set; }

		[Column("level_required"), DefaultValue(0)]
		public int LevelRequired { get; set; }

		[Column("guild_id"), ForeignKey("Guild")]
		public long GuildId { get; set; }

		[Column("guild")]
		public virtual Guild Guild { get; set; }
	}
}
