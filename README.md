# ANoiseGPU

> ANoiseGPU是一个模块化的高性能复杂噪声生成库，支持生成2D，3D，4D噪声，基于我的使用CPU的[ANoise](https://github.com/zhl-dru/ANoise)，将计算部分转移到计算着色器中。

ANoiseGPU目前支持`PERLIN`和`SIMPLEX`噪声，支持`FBM`，`BILLOWY`和`RIDGED`分形，[ANoise](https://github.com/zhl-dru/ANoise)中的其他混合模块都已经完成移植，ANoiseGPU的用法与[ANoise](https://github.com/zhl-dru/ANoise)基本保持一致，在Example中有一个场景展示了所有模块的示例，模块的图像效果与[此处](https://github.com/zhl-dru/ANoise/blob/main/README.md)展示的差别不大。

## 性能参考

GPU非常适合大批量可并行的计算任务，将计算转移到GPU实现了比在CPU上快数倍的计算速度，用于测试的噪声为Octaves=8的`SIMPLEX`的`FBM`分形，对于更小的采样数，计算着色器的调度有其固有的固定开销，更少的计算量几乎不会再减少用时。

| 采样数    | 用时（ms） |
| --------- | ---------- |
| 64*64     | 0.11007    |
| 128*128   | 0.12152    |
| 256*256   | 0.15252    |
| 512*512   | 0.28279    |
| 1024*1024 | 1.01645    |
| 2048*2048 | 3.13426    |
| 4096*4096 | 3.65322    |
| 8192*8192 | 17.36239   |

需要注意的是，很难确定GPU实际的计算用时，以上的用时包括了计算和将数据取回CPU的时间，由于取回数据的速度相对较慢，在大规模的采样中回读数据的时间可能超过计算用时，如果直接在GPU中使用计算结果，可以进一步提升性能。

## 安装

- 下载或克隆此存储库复制到您的项目文件夹中

- 在`Package Manager`中选择`Add Package form git URL`，使用以下链接

  ```
  https://github.com/zhl-dru/ANoiseGPU.git
  ```

## 依赖项

- `com.unity.mathematics` 1.2.6+

## 注意事项

- ANoiseGPU起初是我为特定目的创建的，为了避免不必要的数据传输，采样坐标的计算也转移到计算着色器中，坐标计算定义在`Runtime/Shaders/Kernels/Coord/KCoord.hlsl`和`Runtime/Shaders/Utils/Coord.hlsl`内，2D，3D，4D方法均是从2D坐标展开的，没有非常方便的办法修改这些方法，除了改写这些之外，还必须更改`Runtime/Model/Base/MBase.cs`中与`GPUResolutionData`有关的部分。

- 在使用前，必须使用

  ```c#
  ComputeShader Shader；
  MBase.Shader = Shader；
  ```

  来手动设置计算使用的着色器，应该确保设置的着色器是正确的，计算着色器位于`Runtime/Shaders/ANoiseMain.compute`。

- 与[ANoise](https://github.com/zhl-dru/ANoise)不同，ANoiseGPU的模块调用之后，必须手动释放计算过程中创建的缓存

  ```c#
  MBase module;
  int width;
  ComputeBuffer buffer = module.Get2(width);
  // ......
  MBase.Dispose();
  buffer.Dispose();
  ```

  调用多次后统一释放缓存是相当危险的，可能很快遇到内存不足的问题，如果这么做，将提示一个错误。