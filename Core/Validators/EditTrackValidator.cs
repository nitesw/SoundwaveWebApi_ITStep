using Core.Dtos;
using Data.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validators
{
    public class EditTrackValidator : AbstractValidator<EditTrackDto>
    {
        private readonly SoundwaveDbContext ctx;

        public EditTrackValidator(SoundwaveDbContext ctx)
        {
            this.ctx = ctx;

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100)
                .Must(UniqueTitle).WithMessage("The track with this title is already exists.");
            RuleFor(x => x.AdditionalTags)
                .MaximumLength(40);
            RuleFor(x => x.ArtistName)
                .MaximumLength(20);
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("The description is larger than 1000 symbols.");
        }

        private bool UniqueTitle(EditTrackDto track, string title)
        {
            var dbTitle = ctx.Tracks
                .Where(x => x.Title == title)
                .SingleOrDefault();

            if (dbTitle == null)
                return true;

            return track.Id == dbTitle.Id;
        }
        private static bool LinkMustBeAUri(string? link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
