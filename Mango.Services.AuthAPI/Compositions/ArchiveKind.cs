using System.ComponentModel;

namespace Mango.Services.AuthAPI.Compositions
{
    public enum ArchiveKind
    {
        [Description("Engagement Archive")]
        EngagementArchive = 0,

        [Description("Legal Hold")]
        LegalHold = 1
    }
}
