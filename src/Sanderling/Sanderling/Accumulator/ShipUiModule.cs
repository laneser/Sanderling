﻿using Bib3;
using MemoryStruct = Sanderling.Interface.MemoryStruct;
using Sanderling.Parse;
using System;
using Bib3.Geometrik;

namespace Sanderling.Accumulator
{
	public class ShipUiModuleAndContext : Accumulation.IShipUiModuleAndContext
	{
		public MemoryStruct.IShipUiModule Module { set; get; }

		public Vektor2DInt? Location { set; get; }

		public ShipUiModuleAndContext()
		{
		}
	}

	public class ShipUiModule : EntityScoring<Accumulation.IShipUiModuleAndContext, Parse.IMemoryMeasurement>, Accumulation.IShipUiModule
	{
		public FieldGenMitIntervalInt64<IModuleButtonTooltip> TooltipLast { private set; get; }

		public MemoryStruct.IShipUiModule RepresentedInstant => LastInstant?.Wert?.Module;

		public bool? ModuleButtonVisible => RepresentedInstant?.ModuleButtonVisible;

		public MemoryStruct.IObjectIdInMemory ModuleButtonIconTexture => RepresentedInstant?.ModuleButtonIconTexture;

		public string ModuleButtonQuantity => RepresentedInstant?.ModuleButtonQuantity;

		public bool RampActive => RepresentedInstant?.RampActive ?? false;

		public int? RampRotationMilli => RepresentedInstant?.RampRotationMilli;

		public bool? HiliteVisible => RepresentedInstant?.HiliteVisible;

		public bool? GlowVisible => RepresentedInstant?.GlowVisible;

		public bool? BusyVisible => RepresentedInstant?.BusyVisible;

		public OrtogoonInt Region => RepresentedInstant?.Region ?? OrtogoonInt.Empty;

		public int? InTreeIndex => RepresentedInstant?.InTreeIndex;

		public MemoryStruct.IUIElement RegionInteraction => RepresentedInstant?.RegionInteraction;

		public int? ChildLastInTreeIndex => RepresentedInstant?.ChildLastInTreeIndex;

		protected override void Accumulated(FieldGenMitIntervalInt64<Accumulation.IShipUiModuleAndContext> Instant, Parse.IMemoryMeasurement Shared)
		{
			base.Accumulated(Instant, Shared);

			var ModuleButtonTooltip = Shared?.ModuleButtonTooltip;

			if ((Instant?.Wert?.Module?.HiliteVisible ?? false) &&
				(Instant?.Wert?.Location).HasValue &&
				null != ModuleButtonTooltip)
			{
				TooltipLast = ModuleButtonTooltip.AsIntervalInt64(Instant);
			}

		}

		/// <summary>
		/// score by distance to last seen Instant.
		/// </summary>
		/// <param name="Instant"></param>
		/// <param name="Shared"></param>
		/// <returns></returns>
		public override int Score(Accumulation.IShipUiModuleAndContext Instant, Parse.IMemoryMeasurement Shared)
		{
			return (int)(10 - ((Instant?.Location - NotDefaultLastInstant?.Wert?.Location)?.Length ?? int.MaxValue));
		}

		ShipUiModule()
		{ }

		public ShipUiModule(
			Int64 Id,
			FieldGenMitIntervalInt64<Accumulation.IShipUiModuleAndContext> Instant)
				: base(Id, Instant)
		{
		}

	}
}