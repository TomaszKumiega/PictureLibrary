﻿using Autofac;
using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DataProviders.Builders;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.LibraryFileService;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryModel.Services.StringEncryption;
using System;

namespace PictureLibraryModel
{
    public class DataModelDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterModel(builder);
            RegisterServices(builder);
            RegisterDataAccess(builder);

            builder.RegisterGeneric(typeof(ImplementationSelector<,>)).As(typeof(IImplementationSelector<,>));

        }

        private void RegisterModel(ContainerBuilder builder)
        {
            builder.RegisterType<Drive>().AsSelf();
            builder.Register<Func<Drive>>(context =>
            {
                return () => { return context.Resolve<Drive>(); };
            });

            builder.RegisterType<LocalImageFile>().AsSelf();
            builder.Register<Func<LocalImageFile>>(context =>
            {
                return () => { return context.Resolve<LocalImageFile>(); };
            });

            builder.RegisterType<Folder>().AsSelf();
            builder.Register<Func<Folder>>(context =>
            {
                return () => { return context.Resolve<Folder>(); };
            });

            builder.RegisterType<Library>().AsSelf();
            builder.Register<Func<Library>>(context =>
            {
                return () => { return context.Resolve<Library>(); };
            });

            builder.RegisterType<Tag>().AsSelf();
            builder.Register<Func<Tag>>(context =>
            {
                return () => { return context.Resolve<Tag>(); };
            });
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryFileService>().As<ILibraryFileService>();
            builder.RegisterType<SettingsProviderService>().As<ISettingsProviderService>().SingleInstance();
            builder.RegisterType<StringEncryptionService>().As<IStringEncryptionService>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();
            builder.RegisterType<FileService>().As<IFileService>();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<DataSource>().As<IDataSource>();
            builder.RegisterType<LocalDataSourceBuilder>().Keyed<IDataSourceBuilder>(-1);

            builder.RegisterType<DataSourceCollection>().As<IDataSourceCollection>();

            builder.RegisterType<LocalImageFileProvider>().AsSelf();
            builder.Register<Func<LocalImageFileProvider>>((context) =>
            {
                var value = context.Resolve<LocalImageFileProvider>();
                return () => { return value; };
            });

            builder.RegisterType<LocalLibraryProvider>().AsSelf();
            builder.Register<Func<LocalLibraryProvider>>((context) =>
            {
                var value = context.Resolve<LocalLibraryProvider>();
                return () => { return value; };
            });

            builder.RegisterType<DataSourceCreator>().As<IDataSourceCreator>();
        }
    }
}
