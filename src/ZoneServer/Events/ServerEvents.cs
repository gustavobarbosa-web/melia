﻿using System;
using Melia.Zone.Skills;
using Melia.Zone.World.Actors;
using Melia.Zone.World.Actors.Characters;

namespace Melia.Zone.Events
{
	/// <summary>
	/// Manager for events occurring on the server, such as players logging
	/// in or killing monsters.
	/// </summary>
	public class ServerEvents
	{
		// Time Events
		//-------------------------------------------------------------------

		/// <summary>
		/// Raised on every heartbeat tick.
		/// </summary>
		public event EventHandler<TimeEventArgs> WorldTick;
		public void OnWorldTick(DateTime now) => WorldTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		/// <summary>
		/// Raised every full real-life second.
		/// </summary>
		public event EventHandler<TimeEventArgs> SecondTick;
		public void OnSecondTick(DateTime now) => SecondTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		/// <summary>
		/// Raised every full real-life minute.
		/// </summary>
		public event EventHandler<TimeEventArgs> MinuteTick;
		public void OnMinuteTick(DateTime now) => MinuteTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		/// <summary>
		/// Raised every full five real-life minutes.
		/// </summary>
		public event EventHandler<TimeEventArgs> FiveMinutesTick;
		public void OnFiveMinutesTick(DateTime now) => FiveMinutesTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		/// <summary>
		/// Raised every full fifteen real-life minutes.
		/// </summary>
		public event EventHandler<TimeEventArgs> FifteenMinutesTick;
		public void OnFifteenMinutesTick(DateTime now) => FifteenMinutesTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		/// <summary>
		/// Raised every full twenty real-life minutes.
		/// </summary>
		public event EventHandler<TimeEventArgs> TwentyMinutesTick;
		public void OnTwentyMinutesTick(DateTime now) => TwentyMinutesTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		/// <summary>
		/// Raised every full thirty real-life minutes.
		/// </summary>
		public event EventHandler<TimeEventArgs> ThirtyMinutesTick;
		public void OnThirtyMinutesTick(DateTime now) => ThirtyMinutesTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		/// <summary>
		/// Raised every full real-life hour.
		/// </summary>
		public event EventHandler<TimeEventArgs> HourTick;
		public void OnHourTick(DateTime now) => HourTick?.Invoke(ZoneServer.Instance, new TimeEventArgs(now));

		// Player Events
		//-------------------------------------------------------------------

		/// <summary>
		/// Raised when a player logged in.
		/// </summary>
		/// <remarks>
		/// This event is raised right after the login was confirmed and
		/// before any more packets are sent to the client or they are
		/// added to the world. This makes it a good time to make 
		/// odifications to the character, but packets sent to the
		/// client might be handled as intended yet.
		/// </remarks>
		public event EventHandler<PlayerEventArgs> PlayerLoggedIn;
		public void OnPlayerLoggedIn(Character character) => PlayerLoggedIn?.Invoke(ZoneServer.Instance, new PlayerEventArgs(character));

		/// <summary>
		/// Raised when a player logged in completely.
		/// </summary>
		/// <remarks>
		/// This event is raised right after we start sending the client
		/// updates about the world, such as monsters and players moving
		/// around. At this point, the client is expected to be fully
		/// loaded and ready to receive whatever you throw at it.
		/// </remarks>
		public event EventHandler<PlayerEventArgs> PlayerReady;
		public void OnPlayerReady(Character character) => PlayerReady?.Invoke(ZoneServer.Instance, new PlayerEventArgs(character));

		/// <summary>
		/// Raised when a player gains a new job or circle.
		/// </summary>
		public event EventHandler<PlayerEventArgs> PlayerAdvancedJob;
		public void OnPlayerAdvancedJob(Character character) => PlayerAdvancedJob?.Invoke(ZoneServer.Instance, new PlayerEventArgs(character));

		/// <summary>
		/// Raised when a player says something in public chat.
		/// </summary>
		public event EventHandler<PlayerChatEventArgs> PlayerChat;
		public void OnPlayerChat(Character character, string message) => PlayerChat?.Invoke(ZoneServer.Instance, new PlayerChatEventArgs(character, message));

		/// <summary>
		/// Raised the level of a player's skill changed.
		/// </summary>
		public event EventHandler<PlayerSkillLevelChangedEventArgs> PlayerSkillLevelChanged;
		public void OnPlayerSkillLevelChanged(Character character, Skill skill) => PlayerSkillLevelChanged?.Invoke(ZoneServer.Instance, new PlayerSkillLevelChangedEventArgs(character, skill));

		/// <summary>
		/// Raised a player gains new items, such as by picking them up
		/// or buying them.
		/// </summary>
		public event EventHandler<PlayerItemEventArgs> PlayerAddedItem;
		public void OnPlayerAddedItem(Character character, int itemId, int amount) => PlayerAddedItem?.Invoke(ZoneServer.Instance, new PlayerItemEventArgs(character, itemId, amount));

		/// <summary>
		/// Raised a player gains loses items, such as by destroying or
		/// selling them.
		/// </summary>
		public event EventHandler<PlayerItemEventArgs> PlayerRemovedItem;
		public void OnPlayerRemovedItem(Character character, int itemId, int amount) => PlayerRemovedItem?.Invoke(ZoneServer.Instance, new PlayerItemEventArgs(character, itemId, amount));

		// Combat Events
		//-------------------------------------------------------------------

		/// <summary>
		/// Raised when one entity kills another.
		/// </summary>
		public event EventHandler<CombatEventArgs> EntityKilled;
		public void OnEntityKilled(ICombatEntity target, ICombatEntity attacker) => EntityKilled?.Invoke(ZoneServer.Instance, new CombatEventArgs(target, attacker));
	}
}
