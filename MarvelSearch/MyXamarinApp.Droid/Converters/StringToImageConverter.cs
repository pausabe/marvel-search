using System;
using System.Globalization;
using Android.Graphics;
using Bumptech.Glide;
using MvvmCross;
using MvvmCross.Converters;
using MvvmCross.Platforms.Android;

namespace MyXamarinApp.Droid.Converters
{
    public class StringToImageConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imageUrl)
            {
                Bitmap bitmap = LoadImageFromUrl(imageUrl);
                return bitmap;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Bitmap LoadImageFromUrl(string url)
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var bitmap = Glide.With(context)
                .AsBitmap()
                .Load(url)
                .Submit()
                .Get();

            return (Bitmap)bitmap;
        }
    }
}