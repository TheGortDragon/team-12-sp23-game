import java.lang.StringBuilder;

public class ComparableScore implements Comparable<ComparableScore>
{
    private String name;
    private int score;

    public ComparableScore(String newName, int newScore) //add functionality for date? earlier scores break ties?
    {
        name = newName;
        score = newScore;
    }

    public String getName()
    {
        return name;
    }

    public int getScoreValue()
    {
        return score;
    }

    public String getScoreText()
    {
        
        return addLeadingZeroes(score);
    }

    public String getDisplayString()
    {
        StringBuilder sb = new StringBuilder();
        sb.append(name);
        sb.append(" - ");
        sb.append(addLeadingZeroes(score));
        return sb.toString();
    }

    public String getStorageString()
    {
        StringBuilder sb = new StringBuilder();
        sb.append(name);
        sb.append(",");
        sb.append(score);
        return sb.toString();
    }

    private String addLeadingZeroes(int score)
    {
        String ms = Integer.toString(score % 1000);
        String s = Integer.toString(score / 1000);
        if(ms.length() < 3)
        {
            int charsToAdd = 3 - ms.length();
            for(int i = 0; i < charsToAdd; i++)
            {
                ms = "0" + ms;
            }
        }
        if(s.length() < 3)
        {
            int charsToAdd = 3 - s.length();
            for(int i = 0; i < charsToAdd; i++)
            {
                s = "0" + s;
            }
        }
        
        return s + "." + ms;
    }

    @Override
    public int compareTo(ComparableScore other)
    {
        if(this.score > other.score)
        {
            return 1;
        }
        else if (this.score < other.score)
        {
            return -1;
        }
        else
        {
            return 0; //scores are equal, add date tiebreaking?
        }
    }
}
