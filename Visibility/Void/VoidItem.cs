﻿using System;
using System.Linq;
using System.Text;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Newtonsoft.Json;

namespace Visibility.Void
{
	public class VoidItem
	{
		[JsonIgnore]
		public string Name
		{
			get => $"{Firstname} {Lastname}";
			set
			{
				var name = value.Split(' ');
				Firstname = name[0].ToUppercase();
				Lastname = name[1].ToUppercase();

				var nameBytes = Encoding.UTF8.GetBytes(Name + '\0');
				NameBytes = nameBytes.Length < 64 ? nameBytes : nameBytes.Take(64).ToArray();
			}
		}

		[JsonIgnore] public byte[] NameBytes { get; set; }

		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string HomeworldName { get; set; }
		public DateTime Time { get; set; }
		public uint HomeworldId { get; set; }
		public string Reason { get; set; }
		public bool Manual { get; set; }
		
		public VoidItem()
		{
			NameBytes = Array.Empty<byte>();
			Firstname = string.Empty;
			Lastname = string.Empty;
			HomeworldName = string.Empty;
			Reason = string.Empty;
		}

		public VoidItem(PlayerCharacter actor, string reason, bool manual) : this()
		{
			Name = actor.Name.TextValue;
			Time = DateTime.Now;
			HomeworldId = actor.HomeWorld.Id;
			HomeworldName = actor.HomeWorld.GameData!.Name;
			Reason = reason;
			Manual = manual;
		}

		public VoidItem(string name, string homeworldName, uint homeworldId, string reason, bool manual) : this()
		{
			Name = name;
			Time = DateTime.Now;
			HomeworldId = homeworldId;
			HomeworldName = homeworldName;
			Reason = reason;
			Manual = manual;
		}

		[JsonConstructor]
		public VoidItem(string firstname, string lastname, string homeworldName, DateTime time, uint homeworldId,
			string reason, bool manual) : this()
		{
			Name = $"{firstname} {lastname}";
			HomeworldName = homeworldName;
			Time = time;
			HomeworldId = homeworldId;
			Reason = reason;
			Manual = manual;
		}
	}
}