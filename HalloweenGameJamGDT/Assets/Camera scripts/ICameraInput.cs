// ICameraInput.cs
public interface ICameraInput
{
    /// <summary>
    /// Called every Update to sample input. returns (yawDelta, pitchDelta, zoomDelta)
    /// </summary>
    (float yaw, float pitch, float zoom) Sample();
}
