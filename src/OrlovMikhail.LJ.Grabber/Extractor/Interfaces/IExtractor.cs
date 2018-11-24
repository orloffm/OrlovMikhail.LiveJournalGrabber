using OrlovMikhail.LJ.Grabber.Client;
using OrlovMikhail.LJ.Grabber.Entities;
using OrlovMikhail.LJ.Grabber.Entities.Other;

namespace OrlovMikhail.LJ.Grabber.Extractor.Interfaces
{
    /// <summary>Скачивает один пост as is, но разворачивая комментарии.</summary>
    public interface IExtractor
    {
        /// <summary>Клиент, используемый данным экстрактором.
        /// Устанавливается в конструкторе.</summary>
        ILJClient Client { get; }

        EntryPage GetFrom(LiveJournalTarget url, ILJClientData clientData);

        /// <summary>Заполняет объект поста.</summary>
        bool AbsorbAllData(EntryPage freshSource, ILJClientData clientData, ref EntryPage dumpData);
    }
}
