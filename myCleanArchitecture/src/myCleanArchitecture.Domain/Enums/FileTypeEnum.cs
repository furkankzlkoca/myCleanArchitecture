
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace myCleanArchitecture.Domain.Enums
{
    public enum FileTypeEnum : byte
    {
        [Display(Name = "Profil Fotoğrafı")]
        [Description("Profil Fotoğrafı")]
        ProfilePicture = 1,
    }
}
