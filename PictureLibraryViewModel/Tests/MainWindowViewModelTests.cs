﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PictureLibraryViewModel.Tests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void WindowState_ShouldSetaValue()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(new Commands.CommandFactory());

            // Act
            viewModel.WindowState = System.Windows.WindowState.Maximized;

            // Assert
            Assert.True(viewModel.WindowState == System.Windows.WindowState.Maximized);
        }

        [Fact]
        public void Maximize_ShouldChangeWindowStateToMaximized()
        {
            // Arrange 
            var viewModel = new MainWindowViewModel(new Commands.CommandFactory());
            viewModel.WindowState = System.Windows.WindowState.Normal;

            // Act
            viewModel.Maximize();

            // Assert
            Assert.True(viewModel.WindowState == System.Windows.WindowState.Maximized);
        }

        [Fact]
        public void Maximize_ShouldChangeWindowStateBackToNormal()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(new Commands.CommandFactory());
            viewModel.WindowState = System.Windows.WindowState.Maximized;

            // Act
            viewModel.Maximize();

            // Assert
            Assert.True(viewModel.WindowState == System.Windows.WindowState.Normal);
        }

        [Fact]
        public void Minimize_ShouldChangeWindowStateToMinimized()
        {
            // Arrange
            var viewModel = new MainWindowViewModel(new Commands.CommandFactory());
            viewModel.WindowState = System.Windows.WindowState.Normal;

            // Act
            viewModel.Minimize();

            // Assert
            Assert.True(viewModel.WindowState == System.Windows.WindowState.Minimized);
        }
    }
}
