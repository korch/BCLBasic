using System;

namespace BCLProject
{
    public class CreatedFileEventArgs<TModel> : EventArgs
    {
        public TModel FileItem { get; set; }
    }
}
