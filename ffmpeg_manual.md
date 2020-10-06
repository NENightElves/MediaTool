## 总体
- `-i <input_file>` 指定输入文件，可以指定多个文件，比如一个音频一个视频用来**封装**，可以添加字幕
- `<output_file>` 指定输出文件

## 截取
- `-ss 00:00:15` 指定开始时间
- `-t 00:00:10` 指定长度

## 视频
- `-vcodec (h264|mpeg4|copy)` 指定编码器，`copy`代表与原编码相同
- `-b:v (1500K|1.5M)` 指定视频码率
- `-r 24` 指定视频帧率
- `-vf <filter_name>` 指定过滤器
  - `scale=1024*768` 指定分辨率
  - `transpose=1` 顺时针转90°
  - `hflip` 左右翻转
- `-vn` 禁用视频流，用于**提取音频**
> 若编码器为x264，可以使用`-preset (ultrafast|superfast|veryfast|faster|fast|medium|slow|slower|veryslow|placebo)`，从左至右由快到慢，质量由坏到好。此选项用来调整压制时间与质量之间的关系。

## 音频
- `-ar 48000` 设置音频采样率
- `-ac 1` 设置声道数为1
- `-b:a 256K` 指定音频码率
- `-acodec (aac|flac|mp3|copy)` 指定编码器，`copy`代表与原编码相同
- `-af <filter_name>` 指定过滤器
  - `atempo=(1.5|0.5)` 调整音频播放速度
  - `volume=(2/3|10dB)` 指定音量为2/3或增益10dB
- `-an` 禁用音频流，用于**提取视频**

## 帮助
```
帮助: `--help`
可用的bit流: `–bsfs
可用的编解码器: `–codecs`
可用的解码器: `–decoders`
可用的编码器: `–encoders`
可用的过滤器: `–filters`
可用的视频格式: `–formats`
可用的声道布局: `–layouts`
可用的license: `–L`
可用的像素格式: `–pix_fmts`
可用的协议: `-protocals`
```

> 过滤器与过滤器之间使用`,`分隔

## 例子
将视频a.mp4从10秒开始截取至25秒（共15秒），并使用h264进行视频压制，使用slow预设，并转换为b.mkv：`ffmpeg -i a.mp4 -ss 00:00:10 -t 00:00:15 -vcodec h264 -preset slow b.mkv`
将视频a.mp4进行压缩，使其由1080P变为720P，同时码率降低一半，使用slow预设：`ffmpeg -i a.mp4 -vcodec h264 -vf scale=720*1280 -b:v 1000K -preset slow a_out.mp4`
抽取视频流：`ffmpeg -i a.mp4 -an -vcodec copy a_video_only.mp4`
抽取音频流：`ffmpeg -i a.mp4 -vn -acodec copy a.aac`
抽取音频流并转换为mp3：`ffmpeg -i a.mp4 -vn a.mp3`
合并音视频：`ffmpeg -i a.mp3 -i a_video_only.mp4 -vcodec copy -acodec copy a_mix.mp4`
封装转换：`ffmpeg -i a.mp4 -vcodec copy -acodec copy a.mkv`