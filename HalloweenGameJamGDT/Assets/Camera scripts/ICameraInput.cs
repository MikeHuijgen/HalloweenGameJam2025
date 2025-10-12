// ICameraInput.cs
public interface ICameraInput
{
    /// <summary>
    /// Geeft de input delta terug: (yaw, pitch, zoom)
    /// yaw = horizontale draai
    /// pitch = verticale draai
    /// zoom = scroll/zoom input
    /// </summary>
    (float yaw, float pitch, float zoom) Sample();
}
