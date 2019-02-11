using System;
using System.Collections.Generic;
using System.Threading;

public class AgentAspirateur
{
    public Thread thread;
    private Capteur capteur;
    private Effecteur effecteur;
    private GestionConsole gc;

    private ArbreExploration arbre;
    private const int LIMIT = 6; // profondeur max

    private Pièce[,] environnement; // Belief
    private int performance = 100; // Desire : le meilleur score
    private List<string> listeActions = new List<string>(); // Intentions

    public AgentAspirateur(ManoirEnvironnement env, GestionConsole gc)
    {
        thread = new Thread(new ThreadStart(ThreadLoop));
        this.gc = gc;
        capteur = new Capteur(env);
        effecteur = new Effecteur(env);
        arbre = new ArbreExploration();
    }
        

    private void Explorer()
    {
        gc.AddConsole("L'Agent commence a explorer");

        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        Noeud racine = new Noeud(environnement, performance,0,capteur.getPosX(), capteur.getPosY(), "start",new List<String>(),capteur.getNBLignes());

        Noeud n =  arbre.Explorer(racine, LIMIT, gc);

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;

        gc.AddConsole("L'Agent a fini d'explorer , temps : " +elapsedMs.ToString());
        gc.AddConsole("Meilleur performance trouvée : " + n.performance);
        gc.AddConsole("Liste des actions : ");

        string actions = "[";
        foreach (string s in n.listeActions) actions += s + ",";
        gc.AddConsole(actions+"]");

        listeActions = n.listeActions;
    }

    private void ExplorerGreedy()
    {
        gc.AddConsole("L'Agent commence a explorer ( greedy search )");

        var watch = System.Diagnostics.Stopwatch.StartNew();

        Noeud racine = new Noeud(environnement, performance, 0, capteur.getPosX(), capteur.getPosY(), "start", new List<String>(), capteur.getNBLignes());

        Noeud n = arbre.ExplorerGreedy(racine, gc); // exploration informée GreedySearch

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;

        gc.AddConsole("L'Agent a fini d'explorer , temps : " + elapsedMs.ToString());
        gc.AddConsole("Liste des actions : ");

        string actions = "[";
        foreach (string s in n.listeActions) actions += s + ",";
        gc.AddConsole(actions + "]");

        listeActions = n.listeActions;
    }

    public void Agir()
    {
        listeActions.RemoveAt(0); // on enleve le start
        foreach(string s in listeActions)
        {
            gc.AddConsole("L'agent effectue l'action :" + s);
            switch (s)
            {
                case "gauche":
                    effecteur.DeplacerGauche();
                    break;
                case "droite":
                    effecteur.DeplacerDroite();
                    break;
                case "haut":
                    effecteur.DeplacerHaut();
                    break;
                case "bas":
                    effecteur.DeplacerBas();
                    break;
                case "aspirer":
                    effecteur.NettoyerPiece();
                    break;
                case "ramasser":
                    effecteur.RecupererBijoux();
                    break;
            }
            Thread.Sleep(1000);
        }
        listeActions = null;
    }

    public void ThreadLoop()
    {
        // Tant que le thread n'est pas tué, on travaille
        while (Thread.CurrentThread.IsAlive)
        {
            //Observer l'environnement  UpdateMyState()
            environnement = capteur.getEnvironnement();
            int perfEnv = capteur.getPerformance();
            gc.AddConsole("Performance donnée par l'env : " + perfEnv);

            // Choisir plan ChooseAnAction()
            Explorer();
            Agir();
        }
    }
}
