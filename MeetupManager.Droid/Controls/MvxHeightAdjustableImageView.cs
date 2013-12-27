using System;
using System.Threading.Tasks;

using Android.Content;
using Android.Graphics;
using Android.Renderscripts;
using Android.Runtime;
using Android.Util;

using Cirrious.MvvmCross.Binding.Droid.Views;

namespace MeetupManager.Droid.Controls
{
    /// <summary>
    /// This is a magical adjustable Image View that derives from MvxImageView
    /// This allows you to specify specific height ratio vs width
    /// So if you do local:ratio=".5" then the height will be half the width
    /// It default to 1 so it is a perfect square.
    /// Also you can blur the image if you enable it as well.
    /// </summary>
    public class MvxHeightAdjustableImageView : MvxImageView
    {
        private float m_WidthRatio = 1.0f;
        private bool m_BlurImage = false;
        private float m_BlurRadius = 12.0f;

        private Context m_Context;

        public MvxHeightAdjustableImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public MvxHeightAdjustableImageView(Context context)
			: base(context)
        {
            this.Initialize(context, null);
        }
		

        private void Initialize(Context context, IAttributeSet attrs)
        {

            m_Context = context;
            try
            {
                var values = context.ObtainStyledAttributes(attrs, Resource.Styleable.MvxHeightAdjustableImageView);

                m_WidthRatio = values.GetFloat(Resource.Styleable.MvxHeightAdjustableImageView_ratio, 1.0f);
                if((int)Android.OS.Build.VERSION.SdkInt >= 17)//on 17+!
                    m_BlurImage = values.GetBoolean(Resource.Styleable.MvxHeightAdjustableImageView_blur, false);

                m_BlurRadius = values.GetFloat(Resource.Styleable.MvxHeightAdjustableImageView_blur_radius, 12.0f);
                values.Recycle();
                this.Invalidate();
            }
            catch (Exception ex )
            {
            }

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            this.SetMeasuredDimension(this.MeasuredWidth, (int)(this.MeasuredWidth * m_WidthRatio));
        }

        public override void SetImageBitmap(Android.Graphics.Bitmap inputBitmap)
        {
            base.SetImageBitmap(inputBitmap);
            if (m_BlurImage && inputBitmap != null)
            {
                m_BlurImage = false;

                Task.Factory.StartNew(
                    () => this.CreateBlurredImage(inputBitmap)).ContinueWith(
                            task =>
                                {
                                    if (task.Result != null)
                                        base.SetImageBitmap(task.Result);
                                },
                            TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private Bitmap CreateBlurredImage(Bitmap inputBitmap)
        {
            try
            {
                var rs = RenderScript.Create(m_Context);
                var theInstrinsic = ScriptIntrinsicBlur.Create(rs, Element.U8_4(rs));

                var outputBitmap = Bitmap.CreateBitmap(inputBitmap.Width, inputBitmap.Height, inputBitmap.GetConfig());

                var tmpIn = Allocation.CreateFromBitmap(rs, inputBitmap);
                var tmpOut = Allocation.CreateFromBitmap(rs, outputBitmap);

                theInstrinsic.SetRadius(m_BlurRadius);
                theInstrinsic.SetInput(tmpIn);
                theInstrinsic.ForEach(tmpOut);

                tmpOut.CopyTo(outputBitmap);
                rs.Destroy();
                return outputBitmap;
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}