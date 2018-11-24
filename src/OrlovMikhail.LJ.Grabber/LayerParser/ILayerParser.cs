using OrlovMikhail.LJ.Grabber.Entities;

namespace OrlovMikhail.LJ.Grabber.LayerParser
{
    /// <summary>Parses rendered page as data.</summary>
    public interface ILayerParser
    {
        /// <summary>Parses content as an entry page.</summary>
        EntryPage ParseAsAnEntryPage(string content);

        string Serialize(EntryPage ep);
    }
}