using System.ComponentModel;

namespace WudFilmApp.Models
{
    /// <summary>
    /// The Subcommittee enum describes the possible subcommittees from which a movie selection can originate. 
    /// </summary>
    public enum Subcommittee
    {
        //HACK: Trying to get custom description to display in drop down. 
        [Description("Holly WUD")]
        [DisplayName("Holly WUD")]
        HollyWUD,
     
        International,

        Alternative,

        [Description("Marquee After Dark")]
        MarqueeAfterDark,

        Collaboration,

        [Description("Advanced Screening")]
        AdvancedScreening
    }
}