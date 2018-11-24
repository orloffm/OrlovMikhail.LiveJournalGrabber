using System.IO.Abstractions;
using Autofac;
using OrlovMikhail.LJ.Grabber.Client;
using OrlovMikhail.LJ.Grabber.Entities.Helpers;
using OrlovMikhail.LJ.Grabber.Entities.Helpers.Interfaces;
using OrlovMikhail.LJ.Grabber.Extractor;
using OrlovMikhail.LJ.Grabber.Extractor.Interfaces;
using OrlovMikhail.LJ.Grabber.LayerParser;
using OrlovMikhail.LJ.Grabber.PostProcess;
using OrlovMikhail.LJ.Grabber.PostProcess.Files;
using OrlovMikhail.LJ.Grabber.PostProcess.Filter;
using OrlovMikhail.LJ.Grabber.PostProcess.Userpics;

namespace OrlovMikhail.LJ.Grabber
{
    public class GrabberContainerHelper
    {
        public static void RegisterDefaultClasses(ContainerBuilder builder)
        {
            builder.RegisterType<LJClient>()
                .As<ILJClient>()
                .SingleInstance();
            builder.RegisterType<Worker>()
                .As<IWorker>()
                .SingleInstance();

            builder.RegisterType<FileSystem>()
                .As<IFileSystem>();
            builder.RegisterType<FileStorage>()
                .As<IFileStorage>();
            builder.RegisterType<FileStorageFactory>()
                .As<IFileStorageFactory>();
            builder.RegisterType<FileUrlExtractor>()
                .As<IFileUrlExtractor>();
            builder.RegisterType<UserpicStorageFactory>()
                .As<IUserpicStorageFactory>();
            builder.RegisterType<RelatedDataSaver>()
                .As<IRelatedDataSaver>();
            builder.RegisterType<SuitableCommentsPicker>()
                .As<ISuitableCommentsPicker>();

            builder.RegisterType<OtherPagesLoader>()
                .As<IOtherPagesLoader>();
            builder.RegisterType<EntryBaseHelper>()
                .As<IEntryBaseHelper>();
            builder.RegisterType<EntryHelper>()
                .As<IEntryHelper>();
            builder.RegisterType<EntryPageHelper>()
                .As<IEntryPageHelper>();
            builder.RegisterType<RepliesHelper>()
                .As<IRepliesHelper>();
            builder.RegisterType<LayerParser.LayerParser>()
                .As<ILayerParser>();
            builder.RegisterType<Extractor.Extractor>()
                .As<IExtractor>();
        }
    }
}