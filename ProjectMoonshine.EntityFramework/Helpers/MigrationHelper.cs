using System.Collections.Generic;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Helpers
{
    public static class MigrationHelper
    {
        public static void RunMigrations(this ApplicationDbContext context, List<string> migrationsApplied)
        {
            if (migrationsApplied.Contains("20181016201700_ExamColumnChanges"))
            {
                context.SeedExamsAndEvents();
            }
        }
        public static void SeedData(this ApplicationDbContext context)
        {
        }

        private static void SeedExamsAndEvents(this ApplicationDbContext context)
        {
            context.Events.AddRange(new List<Event>()
                {
                    new Event
                    {
                        EvtName = "Dirty Gauntlet",
                        EvtDescription =@"Lorem ipsum dolor sit amet, sed commodo scribentur at, pri te causae impedit fuisset, ei vix ludus offendit voluptatum. Vis in causae offendit voluptaria. Ancillae scaevola perpetua qui ea. Falli graecis vulputate sea cu, natum fugit dolores ut per."
                    },
                    new Event
                    {
                        EvtName = "Moonstone Tamandua",
                        EvtDescription ="Lorem ipsum dolor sit amet, an mucius partiendo iudicabit vim, nam at regione alterum placerat. Vidit quando detraxit an cum. Habeo omittam eum an, fugit assentior vulputate vel ex. Iuvaret temporibus id sea, et timeam alterum iracundia per."
                    },
                    new Event
                    {
                        EvtName = "Chrome Satan",
                        EvtDescription ="Lorem ipsum dolor sit amet, ad movet inermis instructior vis, et mel solum discere tincidunt, cu sint nibh definitiones per. Eos luptatum intellegebat ea, vel ut primis vivendo. Ei harum prompta mei, sit in labore appetere expetendis, sanctus fabulas antiopam cu eos. Facilis gloriatur in eum. Ei sea consul percipitur. Vis erroribus consetetur ut, etiam urbanitas incorrupte ex mei."
                    },
                    new Event
                    {
                        EvtName = "Salty Yeast",
                        EvtDescription ="Lorem ipsum dolor sit amet, mazim habemus et ius. Ne facer inimicus gubergren pro, salutandi definiebas mel at. Modo mentitum qui an, te vis impedit detracto. Duis reprimique id usu. At vis ridens feugiat voluptua, tale legere cu vis."
                    },
                    new Event
                    {
                        EvtName = "Lost Ooze",
                        EvtDescription ="Lorem ipsum dolor sit amet, dicat integre eum no. Vel id wisi insolens, ex enim putant ius, nec id error inciderint. Ad est iusto offendit democritum, eam liber congue et. Cum no atqui mediocritatem, mel in alia sint."
                    },
                    new Event
                    {
                        EvtName = "Roads Coasters",
                        EvtDescription ="Lorem ipsum dolor sit amet, cu dolorum neglegentur eos. Et cum duis numquam efficiendi. Per porro mollis debitis ne, quo ne reque accusamus complectitur, vis ei vero possim cetero. Mel ad facete feugiat. Eu eum fabulas dolorem, ut tation necessitatibus vel. Nostrud epicurei cum in."
                    },
                    new Event
                    {
                        EvtName = "Crystal Connection",
                        EvtDescription ="Lorem ipsum dolor sit amet, dissentias disputando te has, ad vix causae detraxit theophrastus, ex mea alii errem eloquentiam. Ut essent propriae periculis eos, ius quis possit ei, nec te iusto signiferumque. Pri case officiis in. Persius graecis mel ad. Ut audiam option legimus mel, te cum iuvaret pertinax torquatos."
                    },
                    new Event
                    {
                        EvtName = "Lucky Wonderland",
                        EvtDescription ="Lorem ipsum dolor sit amet, vim sonet ridens instructior an, pro ad ferri inani menandri. Eos id principes assueverit. Etiam voluptatum at pri, ex vis adversarium consequuntur necessitatibus, quo veniam decore quodsi ex. Vim ea adolescens dissentiet, quis consul oblique et his. Cu pri doming noster neglegentur, vim in unum idque alterum. Per habeo ubique ad, id hinc iriure voluptaria usu. Ea epicuri molestie ius, his ad duis veniam scripta."
                    },
                    new Event
                    {
                        EvtName = "Superlative Glamorous Factor",
                        EvtDescription ="Lorem ipsum dolor sit amet, ne has aliquip facilis, id pri habeo illud alienum, ea pro vidit admodum commune. Mentitum prodesset eu sea. Nec porro everti copiosae ut. Duo essent scaevola tacimates ad, eum an sale idque definitionem. Eu iriure accusamus ius, vis ei tale erroribus, per tibique albucius te."
                    },
                    new Event
                    {
                        EvtName = "SoftPrinciple",
                        EvtDescription ="Lorem ipsum dolor sit amet, ad eos harum dicunt. Vel ne populo feugiat, sed integre ancillae pertinax in. No sed falli vituperatoribus, dicat atomorum consulatu cum te. His cu erat oportere recteque, at eum nostro omnesque."
                    },

                });
        }
    }
}