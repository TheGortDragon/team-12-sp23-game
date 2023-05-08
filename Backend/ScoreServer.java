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
import java.lang.StringBuilder;


import java.net.InetSocketAddress;
import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import com.sun.net.httpserver.HttpServer;
import com.sun.net.httpserver.Headers;

public class ScoreServer
{
    private final int MAX_SCORES = 10;
    private ArrayList<ComparableScore> scores = new ArrayList<ComparableScore>();

    public static ScoreServer ss;
    
    public static void main(String[] args)
    {
        ss = new ScoreServer();

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
                    ss.writeToFile();
                    ss.displayScores();

                    response = "Data received.";
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
        for(int i = 0; i < MAX_SCORES; i++)
        {
            addScore("AAAAAAAA", 99999999);
        }
        recallFromFile();
    }

    public void addScore(String name, int score)
    {
        ComparableScore newScore = new ComparableScore(name, score);
        scores.add(newScore);
        Collections.sort(scores); //Collections.sort(scores, Collections.reverseOrder()); if order is reversed
    }

    public void addScore(ComparableScore score)
    {
        scores.add(score);
        Collections.sort(scores);
    }

    public String getNames()
    {
        StringBuilder sb = new StringBuilder();
        sb.append(scores.get(0).getName());
        for(int i = 1; i < MAX_SCORES; i++)
        {
            sb.append(",");
            sb.append(scores.get(i).getName());
        }
        return sb.toString();
    }

    //Gets the scores as a string of comma separated numbers
    public String getScores()
    {
        StringBuilder sb = new StringBuilder();
        sb.append(scores.get(1).getScoreText());
        for(int i = 1; i < MAX_SCORES; i++)
        {
            sb.append(",");
            sb.append(scores.get(i).getScoreText());
        }
        return sb.toString();
    }

    //Writes the scoreboard to the console
    public void displayScores()
    {
        StringBuilder sb = new StringBuilder();
        sb.append(scores.get(0).getDisplayString());
        for(int i = 1; i < MAX_SCORES; i++)
        {
            sb.append("\n");
            sb.append(scores.get(i).getDisplayString());
            
        }
        System.out.println(sb.toString());
    }

    public String getScoresAsJSON()
    {
        StringBuilder sb = new StringBuilder();
        sb.append("{\n\"Names\":\"");
        sb.append(getNames());
        sb.append("\",\n\"Scores\":\"");
        sb.append(getScores());
        sb.append("\"\n}");
        return sb.toString();
    }

    public ComparableScore parseJSON(String json)
    {
        json = json.replaceAll(" ", "");
        int start = json.indexOf("\"Name\":\"") + 8;
        int end = json.indexOf("\",");
        String nameString = json.substring(start, end);

        start = json.indexOf("\"Score\":") + 8;
        end = json.indexOf("}") - 2;
        String scoreString = json.substring(start, end);
        int score = Integer.valueOf(scoreString);
        return new ComparableScore(nameString, score);
    }

    public void writeToFile()
    {
        try{
            File file = new File("scores");
            file.createNewFile(); //create the file if it does not exist
            FileWriter fw = new FileWriter(file, false);
            PrintWriter pw = new PrintWriter(fw);
            pw.append(scores.get(0).getStorageString());
            for(int i = 1; i < scores.size(); i++)
            {
                pw.append("\n");
                pw.append(scores.get(i).getStorageString());
            }
            pw.close();
            fw.close();

        } catch(IOException e)
        {
            System.out.println("File operation failed.");
        }
    }

    public void recallFromFile()
    {
        try{
            ArrayList<ComparableScore> newScores = new ArrayList<ComparableScore>();
            File file = new File("scores");
            Scanner s = new Scanner(file);
            while(s.hasNextLine())
            {
                String[] data = s.nextLine().split(",");
                String name = data[0];
                int score = Integer.valueOf(data[1]);
                newScores.add(new ComparableScore(name, score));
            }
            scores = newScores;
            s.close();

        } catch(IOException e)
        {
            System.out.println("File operation failed.");
        }
    }
}