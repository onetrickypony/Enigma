

//Зачем нужен рефлектор?
//Правило рефлектора:
using System;

public class Rotor
{
    private byte byte_pos;
    private Rotor previous, next;
    private byte[] byte_sequence;
    private byte round;


	public Rotor(byte byte_pos, byte[] byte_sequence)
	{
        this.byte_pos = byte_pos;
        this.byte_sequence = byte_sequence;
        round = 0;
    }

	public void SetNextRotor(Rotor next){
		this.next = next;
	}
	public void SetPreviousRotor(Rotor previous){
		this.previous = previous;
	}

    public void SetRotorPos(byte rotorPos)
    {
        byte_pos = rotorPos;
        round = 0;
    }

    public void Move()
    {
        if (byte_pos == 255)
            byte_pos = 0;
        else
            byte_pos++;

        if (round == 1)
        {
            round++;
            round--; 
        }

        if (round == 255)
        {
            round = 0;

            if (next != null)
                next.Move();
        }
        else
        {
            round++;
        }
    }

    public byte ForwardPass(byte b)
    {
        int res;

        if (previous == null)
            res = byte_sequence[(byte_pos + b)%256];
        else
        {
            res = byte_sequence[(b + (byte_pos - previous.byte_pos) + 256)%256];
        }

        if (next != null)
        {
            res = next.ForwardPass((byte)res);
        }

        if (next == null)
        {
            res = (res - byte_pos + 256) % 256;
        }

        return (byte)res;
    }

    
    public byte BackPass(byte b)
    {
        int res;

        if (next == null)
        {
            res = SearchPos((byte_pos + b) % 256);
        }
        else
        {
            res = SearchPos((b - (next.byte_pos - byte_pos) + 256) % 256);
        }

        if (previous != null)
        {
            res = previous.BackPass((byte)res);
        }

        if (previous == null)
        {
            res = (res - byte_pos + 256) % 256;
        }

        return (byte)res;

    }

    private int SearchPos(int b)
    {
        for (int i = 0; i < byte_sequence.Length; i++)
        {
            if (byte_sequence[i] == b)
            {
                b = i;
                break;
            }
        }

        return b;
    }

}

