import java.io.OutputStream;
import java.io.InputStream;
import java.io.IOException;
import java.io.File;
import java.io.FileWriter;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Scanner;

import javax.swing.RepaintManager;

import java.lang.StringBuilder;


import java.net.InetSocketAddress;
import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import com.sun.net.httpserver.HttpServer;
import com.sun.net.httpserver.Headers;

public class ScoreServer
{
    public static final int MAX_SCORES = 10;
    public static final int LEVEL_COUNT = 4;
    private ArrayList<ArrayList<ComparableScore>> scores = new ArrayList<ArrayList<ComparableScore>>();
    //private ArrayList<ComparableScore> scores = new ArrayList<ComparableScore>();

    public static ScoreServer ss;
    
    public static void main(String[] args)
    {
        ss = new ScoreServer();

        if(args.length == 1)
        {
            if(args[0].equals("clear"))
            {
                for(int i = 1; i <= ScoreServer.LEVEL_COUNT; i++)
                {
                    ss.clearScoresOnFile(i);
                }
            }
            
        }

        ScoreServer scoreServer = new ScoreServer();
        try {
            HttpServer server = HttpServer.create(new InetSocketAddress(9000), 0);
            server.createContext("/scores", new GameHandler());
            server.setExecutor(null);
            server.start();

            boolean running = true;
            int count = 0;
            while(running)
            {
                try{
                    Thread.sleep(5000);
                    System.out.println("Still alive. Count: " + count);
                    count++;
                } catch(InterruptedException e)
                {

                }
            }

        } catch(IOException e)
        {
            System.out.println("Failed to create web server.");
        }
    }

    static class GameHandler implements HttpHandler
    {
        @Override
        public void handle(HttpExchange t) throws IOException
        {
            System.out.println("Receiving request...");
            String method = t.getRequestMethod();
            String response;
            OutputStream os;
            InputStream is;
            switch(method)
            {
                case "POST": //receive data from client
                    System.out.println("Method identified as POST.");

                    StringBuilder sb = new StringBuilder();
                    is = t.getRequestBody();
                    int i;
                    while((i = is.read()) != -1)
                    {
                        sb.append((char) i);
                    }
                    is.close();
                    System.out.println("Received data:\n" + sb.toString());

                    ComparableScore newScore = ss.parseJSON(sb.toString());
                    ss.addScore(newScore);
                    ss.writeToFile(newScore.getLevel());
                    ss.displayScores(newScore.getLevel());
                    

                    System.out.println("Sending response:");

                    response = ss.getScoresAsJSON();
                    System.out.println(response);

                    t.getResponseHeaders().add("Access-Control-Allow-Origin", "*");
                    t.sendResponseHeaders(200, response.getBytes().length);
                    os = t.getResponseBody();
                    os.write(response.getBytes());
                    os.close();
                    
                    break;
                case "GET": //send data to client
                    System.out.println("Method identified as GET");

                    response = ss.getScoresAsJSON();
                    t.getResponseHeaders().add("Access-Control-Allow-Origin", "*");
                    t.sendResponseHeaders(200, response.getBytes().length);
                    os = t.getResponseBody();
                    os.write(response.getBytes());
                    os.close();
                    break;
                default:
                    System.out.println("Unsupported request method.");
                    break;
            }
        }
    }

    public ScoreServer()
    {
        
        for(int i = 0; i < LEVEL_COUNT; i++)
        {
            scores.add(new ArrayList<ComparableScore>());
        }
        for(int i = 0; i < LEVEL_COUNT; i++)
        {
            recallFromFile(i + 1);
        }
    }

    public void addScore(String name, int score, int level)
    {
        ComparableScore newScore = new ComparableScore(name, score, level);
        scores.get(level - 1).add(newScore);
        Collections.sort(scores.get(level - 1)); //Collections.sort(scores, Collections.reverseOrder()); if order is reversed
    }

    public void addScore(ComparableScore score)
    {
        scores.get(score.getLevel() - 1).add(score);
        ArrayList<ComparableScore> theseScores = scores.get(score.getLevel() - 1);
        Collections.sort(theseScores);
        scores.set(score.getLevel() - 1, theseScores);
        
    }

    public String getNames(int level)
    {
        StringBuilder sb = new StringBuilder();
        sb.append(scores.get(level - 1).get(0).getName());
        for(int i = 1; i < MAX_SCORES; i++)
        {
            sb.append(",");
            sb.append(scores.get(level - 1).get(i).getName());
        }
        return sb.toString();
    }

    //Gets the scores as a string of comma separated numbers
    public String getScores(int level)
    {
        StringBuilder sb = new StringBuilder();
        sb.append(scores.get(level - 1).get(0).getScoreText());
        for(int i = 1; i < MAX_SCORES; i++)
        {
            sb.append(",");
            sb.append(scores.get(level - 1).get(i).getScoreText());
        }
        return sb.toString();
    }

    //Writes the scoreboard to the console
    public void displayScores(int level)
    {
        StringBuilder sb = new StringBuilder();
        sb.append(scores.get(level - 1).get(0).getDisplayString());
        for(int i = 1; i < MAX_SCORES; i++)
        {
            sb.append("\n");
            sb.append(scores.get(level - 1).get(i).getDisplayString());
            
        }
        System.out.println(sb.toString());
    }

    public String getScoresAsJSON()
    {
        StringBuilder sb = new StringBuilder();
        sb.append("{");
        for(int i = 1; i <= LEVEL_COUNT; i++)
        {
            sb.append("\n\"Names" + i + "\":\"");
            sb.append(getNames(i));
            sb.append("\",\n\"Scores" + i +  "\":\"");
            sb.append(getScores(i));
            sb.append("\",");
        }
        sb.setLength(sb.length() - 1);
        sb.append("\n}");
        return sb.toString();
    }

    public ComparableScore parseJSON(String json)
    {
        json = json.replaceAll(" ", "");
        json = json.replaceAll("[\r\n]", "");
        int start = json.indexOf("\"Name\":\"") + 8;
        int end = json.indexOf("\",", start);
        String nameString = json.substring(start, end);

        start = json.indexOf("\"Score\":") + 8;
        end = json.indexOf(",", start);
        String scoreString = json.substring(start, end);
        int score = Integer.valueOf(scoreString);

        start = json.indexOf("\"Level\":") + 8;
        end = json.indexOf("}", start);
        String levelString = json.substring(start, end);
        int level = Integer.valueOf(levelString);
        return new ComparableScore(nameString, score, level);
    }

    public void writeToFile(int level)
    {
        try{
            File file = new File("scores" + level);
            file.createNewFile(); //create the file if it does not exist
            FileWriter fw = new FileWriter(file, false);
            PrintWriter pw = new PrintWriter(fw);
            pw.append(scores.get(level - 1).get(0).getStorageString());
            for(int i = 1; i < scores.get(level - 1).size(); i++)
            {
                pw.append("\n");
                pw.append(scores.get(level - 1).get(i).getStorageString());
            }
            pw.close();
            fw.close();

        } catch(IOException e)
        {
            System.out.println("File operation failed.");
        }
    }

    public void clearScoresOnFile(int level)
    {
        //scores.clear();
        for(int i = 0; i < LEVEL_COUNT; i++)
        {
            scores.set(level - 1, new ArrayList<ComparableScore>());
        }
        for(int i = 0; i < MAX_SCORES; i++)
        {
            addScore("AAAAAAAA", 99999999, level);
        }
        try{
            File file = new File("scores" + (level));
            file.createNewFile(); //create the file if it does not exist
            FileWriter fw = new FileWriter(file, false);
            PrintWriter pw = new PrintWriter(fw);
            pw.append(scores.get(level - 1).get(0).getStorageString());
            for(int i = 1; i < MAX_SCORES; i++)
            {
                pw.append("\n");
                pw.append(scores.get(level - 1).get(i).getStorageString());
            }
            pw.close();
            fw.close();

        } catch(IOException e)
        {
            System.out.println("File operation failed.");
        }
    }

    public void recallFromFile(int level)
    {
        try{
            ArrayList<ComparableScore> newScores = new ArrayList<ComparableScore>();
            File file = new File("scores" + level);
            Scanner s = new Scanner(file);
            while(s.hasNextLine())
            {
                String[] data = s.nextLine().split(",");
                String name = data[0];
                int score = Integer.valueOf(data[1]);
                newScores.add(new ComparableScore(name, score, level));
            }
            scores.set(level - 1, newScores);
            s.close();

        } catch(IOException e)
        {
            System.out.println("File operation failed.");
        }
    }
}