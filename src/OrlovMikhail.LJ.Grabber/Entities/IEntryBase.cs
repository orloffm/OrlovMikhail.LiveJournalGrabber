using System;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    public interface IEntryBase
    {
        DateTime? Date { get; set; }

        long Id { get; set; }

        UserLite Poster { get; set; }

        Userpic PosterUserpic { get; set; }

        string Subject { get; set; }

        string Text { get; set; }

        string Url { get; set; }
    }
}