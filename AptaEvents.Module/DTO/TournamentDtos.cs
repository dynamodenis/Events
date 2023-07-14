namespace AptaEvents.Module.DTO
{
	public class TournamentListingDto
	{
		public int TournamentID { get; set; } = 0;
		public string TournamentName { get; set; } = string.Empty;
	}

	public class TournamentDataDto
	{
		public int EventId { get; set; } = 0;
		public int TournamentId { get; set; }
		public string? TournamentName { get; set; }
		public bool EventScoringFlag { get; set; } = false; // (Event: DisplayFlag)
		public bool TournamentScoringFlag { get; set; } = false;// (DisplayFlag)
		public bool ShowWaitingListFlag { get; set; }
		public int? SeasonId { get; set; }
		public string? SeasonName { get; set; } // format of: 2023-2024	(Season: SeasonName)
		public int? Region { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? TournamentType { get; set; } // (TournamentType: TournamentType)
		public int Capacity { get; set; }
		public bool CancelledFlag { get; set; } = false;
		public bool EntryOpenFlag { get; set; } = false; // (ShowOnlineFlag)
		public bool NRTFlag { get; set; } = false;
		public bool AptaTourFlag { get; set; } = false;
		public bool GrandPrixFlag { get; set; } = false;
		public bool NationalChampionshipFlag { get; set; } = false;
		public bool PTIFlag { get; set; } = false;
		public bool JuniorFlag { get; set; } = false;
		public bool MastersFlag { get; set; } = false;
		public DateTime? EntryOpenDate { get; set; } // OpenDate
		public DateTime? EntryCloseDate { get; set; } // CloseDate
	}
}
