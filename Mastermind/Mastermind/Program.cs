/** Mastermind
 * Trouver la bonne combinaison de couleur
 * Date : 28/12/2019
 * Author : Baucheron Romain
 */

using System;

namespace Mastermind
{
    class Program
    {
        /*
        * Controle le caractere
        */
        static bool controleCaractere(char couleur)
        {
            if (couleur == 'R' || couleur == 'N' || couleur == 'B' || couleur == 'J' || couleur == 'V' || couleur == 'G' || couleur == 'M')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
        * Saisie du caractère
        */
        static char saisieCaractere(int ligne, int colonne)
        {
            Console.SetCursorPosition(19 + (colonne * 4), ligne); // Positionnement du curseur
            char couleur = Console.ReadKey().KeyChar; // Enregistre la couleur dans une variable
            if (!controleCaractere(couleur)) // Si la couleur saisie est différente des 7 disponible (et donc la variable booléenne couleur fausse)
            {
                Console.SetCursorPosition(20 + (colonne * 4), ligne); // Place le cursor une colonne plus loin
                Console.Write("\b \b"); // Efface le caractère à la position -1
                return saisieCaractere(ligne, colonne); // Retourne au début du module pour ressaisir un caractère puis le retester
            }
            return couleur; // Retourne la couleur quand elle fait partie des 7 disponible
        }

        /*
        * Remplit le tableau avec les 5 caractères saisies
        */
        static char[] tableauCouleur(int ligne)
        {
            char[] tCouleur = new char[5];
            for (int k = 0; k < 5; k++)
            {
                tCouleur[k] = saisieCaractere(ligne, k);
            }
            return tCouleur;
        }

        /*
        * Calcul des caractères bien placés
        */
        static int bienPlace(char[] vec1, char[] vec2)
        {
            int bienPlace = 0;
            for (int k = 0; k < 5; k++)
            {
                // 2 couleurs identiques à la même position
                if (vec1[k] == vec2[k])
                {
                    bienPlace++;
                    // changement de valeurs pour le pas les recompter
                    vec1[k] = 'X';
                    vec2[k] = 'Y';
                }
            }
            return bienPlace;
        }

        /*
        * Calcul des caractères mal placés
        */
        static int malPlace(char[] vec1, char[] vec2)
        {
            int malPlace = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // 2 couleurs identiques à des positions différentes
                    if (vec1[i] == vec2[j])
                    {
                        malPlace++;
                        // changement des valeurs pour ne pas les recomptées
                        vec1[i] = 'X';
                        vec2[j] = 'Y';
                    }
                }
            }
            return malPlace;
        }

        /*
        * Compliment
        */
        static string compliment(int essai)
        {
            if (essai <= 5)
            {
                return "Bravo";
            }
            else
            {
                if (essai <= 10)
                {
                    return "Correct";
                }
                else
                {
                    return "Décevant";
                }
            }
        }
        static void Main(string[] args)
        {
            // Déclaration
            char[] formule = new char[5]; // Tableau de la formule
            char[] essai = new char[5]; // Tableau des essais du joueur 2
            char[] copieFormule = new char[5];
            int ligne = 0;
            int bp, mp;

            // Saisie de la formule par le joueur 1 dans un tableau
            Console.Write("1er joueur : ");
            formule = tableauCouleur(0);
            Console.Clear(); // Efface la saisie du premier joueur

            // Saisie des couleurs par le joueur 2
            Console.WriteLine("2eme joueur :                             bien placé    mal placé");
            // Boucle sur les essais du second joueur
            do
            {
                ligne++; // incrémentation de la ligne pour passer à l'essai suivant
                Array.Copy(formule, copieFormule, 5); // Sauvegarde de la formule d'origine
                // Saisie de l'essai
                Console.SetCursorPosition(0, ligne);
                Console.Write("essai n°" + ligne + " : ");
                essai = tableauCouleur(ligne);

                // calcul et affichage des bien et mal placés
                Console.SetCursorPosition(47, ligne);
                bp = bienPlace(copieFormule, essai);
                Console.Write(bp);

                Console.SetCursorPosition(60, ligne);
                mp = malPlace(copieFormule, essai);
                Console.Write(mp);
            } while (bp < 5);

            // Affichage final
            Console.WriteLine();
            Console.Write("La formule a été trouvée en " + bp + " essais : " + compliment(ligne));
            Console.ReadLine();
        }
    }
}
