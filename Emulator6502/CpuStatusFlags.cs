public class CpuStatusFlags
{
    public bool C;
    /// <summary>
    /// Reports if last operation returned zero.
    /// </summary>
    public bool Z;
    public bool I;
    public bool D;
    public bool B;
    public bool V;
    /// <summary>
    /// Reports if last operation returned negative value
    /// </summary>
    public bool N;

    public void Reset()
    {
        C = false;
        Z = false;
        I = false;
        D = false;
        B = false;
        V = false;
        N = false;
    }
}