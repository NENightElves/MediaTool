using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MediaTool
{
    class MediaItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public String Path { get; set; }
        public String Directory { get; set; }
        public String Name { get; set; }
        public String Resolution { get; set; }
        public String BitRate { get; set; }
        public String TargetName { get; set; }
        private String _status;
        public String Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
        private String _format;
        public String Format
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
                TargetName = "output_" + Name.Substring(0, Name.LastIndexOf(".")) + "." + Format;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TargetName"));
            }
        }

        public MediaItem(String path)
        {
            Path = path;
            Directory = path.Substring(0, path.LastIndexOf("\\"));
            Name = path.Substring(path.LastIndexOf("\\") + 1);
            Resolution = "";
            BitRate = "";
            Format = path.Substring(path.LastIndexOf(".") + 1);
            Status = "未开始";
        }

    }
}
