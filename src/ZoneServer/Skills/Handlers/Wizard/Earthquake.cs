﻿using System;
using System.Collections.Generic;
using System.Linq;
using Melia.Shared.L10N;
using Melia.Shared.Tos.Const;
using Melia.Shared.World;
using Melia.Zone.Network;
using Melia.Zone.Skills.Combat;
using Melia.Zone.Skills.Handlers.Base;
using Melia.Zone.Skills.SplashAreas;
using Melia.Zone.World.Actors;
using Melia.Zone.World.Actors.CombatEntities.Components;
using static Melia.Zone.Skills.SkillUseFunctions;

namespace Melia.Zone.Skills.Handlers.Swordsman
{
	/// <summary>
	/// Handler for the Wizard skill Earthquake.
	/// </summary>
	[SkillHandler(SkillId.Wizard_EarthQuake)]
	public class Earthquake : IGroundSkillHandler
	{
		/// <summary>
		/// Handles skill, damaging targets.
		/// </summary>
		/// <param name="skill"></param>
		/// <param name="caster"></param>
		/// <param name="originPos"></param>
		/// <param name="farPos"></param>
		public void Handle(Skill skill, ICombatEntity caster, Position originPos, Position farPos)
		{
			if (!caster.TrySpendSp(skill))
			{
				caster.ServerMessage(Localization.Get("Not enough SP."));
				return;
			}

			skill.IncreaseOverheat();
			caster.Components.Get<CombatComponent>().SetAttackState(true);

			// Recalculate origin and far based on the direction and the
			// caster's position
			var direction = caster.Position.GetDirection(farPos);
			var diameter = skill.Properties.GetFloat(PropertyName.WaveLength);
			var radius = diameter / 2f;

			originPos = caster.Position;
			farPos = caster.Position.GetRelative(direction, diameter);

			// Get circular splash area
			var center = caster.Position.GetRelative(direction, radius);
			var splashArea = new Circle(center, radius);

			Debug.ShowShape(caster.Map, splashArea, edgePoints: false);

			// Attack targets
			var targets = caster.Map.GetAttackableEntitiesIn(caster, splashArea);
			var damageDelay = TimeSpan.FromMilliseconds(200);

			var hits = new List<SkillHitInfo>();

			foreach (var target in targets)
			{
				var damage = SCR_CalculateDamage(caster, target, skill);
				target.TakeDamage(damage, caster);

				var hit = new SkillHitInfo(caster, target, skill, damage, damageDelay, TimeSpan.Zero);
				hits.Add(hit);
			}

			var targetHandle = targets.FirstOrDefault()?.Handle ?? 0;

			Send.ZC_SKILL_READY(caster, skill, originPos, farPos);
			Send.ZC_NORMAL.Unkown_1c(caster, targetHandle, originPos, originPos.GetDirection(farPos), Position.Zero);
			Send.ZC_SKILL_MELEE_GROUND(caster, skill, farPos, hits);
		}
	}
}