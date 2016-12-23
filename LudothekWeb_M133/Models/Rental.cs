// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System;

namespace LudothekWeb_M133.Models {
    public class Rental {
        #region Properties

        public int Id { get; set; }
        public int GameId { get; set; }
        public string User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        #endregion
    }
}