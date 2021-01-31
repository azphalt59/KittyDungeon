# Kitty-Dungeon

# Présentation du jeu

Kitty dungeon est un jeu Shooter futuriste 2D en vue top and down avec des mécaniques de rogues like.Le joueur incarnera un petit robot chat qui devra s’échapper d’un donjon futuriste. A chaque partie, ce donjon sera généré de manière procédurale. Le placement des différentes salles ainsi que celui des divers objets seront de ce fait différent à chaque run du joueur.Le but du joueur est de se déplacer dans le donjon et de tuer les ennemis.Pour ce faire, on peut tirer des poisson morts sur les chiens pour les tuer tout en évitant leurs os.

# Direction artistique 

L'effet recherché était de faire des graphismes assez stylisé cartoon/vectoriel dans un aspect assez enfantin. On a entièrement réalisé les sprites du jeu, mais en s'inspirant de certaine références trouvé sur Pinterest/Deviantart.

# Description du fun
 
Le côté décalé ou on incarne un chat cosmonaute qui tire des poissons sur des chiens fait pour moi parti de ce qui rend le jeu "amusant"

# Problème rencontrés 

Régler les problèmes de git nous a un peu ralenti dans notre travail, surtout quand nous avons du recommencer entièrement la manipulation plusieurs fois.
Problème de derniere minute sur git qui empechait le push final du projet. "fetch first" error, en cherchant sur internet j'ai donc utilisé git push origin main --force ce qui a enlevé tout les précédents commit de la branch main 
Difficulés pour la programmation de l'IA / et la génération procédurale qui nous ont pris du temps
Nombreux problèmes de référencement dues aux multiples prefab instanciées.


# Description technique du projet
Le partie la plus longue du code à mettre en place était la génération procédurale du dungeon. 
  La premiere étape à mettre en place était tout d'abord de générer les salles les une à coté des autres. Pour cela on démarre avec une salle "Start" qui a pour coordonnée X=0          Y=0. On génère ensuite une nouvelle salle en ajoutant soit 1 ou -1 à XouY en repartant des coordonnées 0, jusqu'à trouver des coordonnées disponibles. 
  Ensuite, le nombre de salle généré par le dungeon dépend d'un gameObject (scripttable object) qui détermine le nombre de fois que le dungeon va lancer la fonction de création   de salle (itération) ainsi que le nombre de salle qu'il va essayer de créer dans cette fonction (nombre de crawler).
        Exemple : Le gameObject avec 2 crawlers et 10 itérations donnera 20 salles maximum, il      peut en donner moins s'il essaie de créer une salle sur une salle déjà                 existente.
  Dans un troisième temps, on a du faire en sorte que les portes des salles soient désactivées s'il n'y a aucune salle adjacente à la porte. On a donc fait un check de la salle     à adjacente en fonction de la position de la porte (+1X à droite, -1X à gauche, +1Y en haut, -1Y en bas).
  En dernier temps, on a crée plusieurs type de salle qu'on a pu donc mettre dans une liste possible de salles spawnable par le dungeon pour avoir une plus grande diversité dans     le dungeon. En plus de ça, nous avons "forcé" le dungeon à détruire des salles à un moment précis de la génération pour les remplacer par des salles contenant un item.
  
Pour la création des différentes salles nous avons donc juste changer le nombre des différents ennemis présent dans la salle. Les 3 types d'ennemis sont crées sur une base communes avec des statistiques différentes. Ils possèdent des état différents : Idle quand le joueur n'est pas présent dans la salle, Patrol quand le joueur se trouve dans la même salle mais pas à portée de vue, Rush quand le joueur est à portée de vue mais pas à porte d'attaque, Attack quand le joueur est à portée de tir, Death quand sa santé atteint 0
Nous avons du également gérer l'ouverture des portes dans les salles. Les portes se ferment quand le joueur se dirige vers le centre de la salle, un collider se met en place pour interdire l'accès aux autres salles pour le joueur. Pour la réouverture le jeu check s'il n'y a plus aucun ennemi dans la salle. Si c'est le cas les portes peuvent se rouvrir.

Le joueur peut ramasser des items qui modifient ses statistiques (attack speed pour le poisson radioactif, move speed pour la pelotte de laine, damage pour le poisson avec un +)
Les ennemis ont des damages différents, le joueur peut aussi récupérer des briques de lait pour récuperer de la santé.
  
# Ressenti personnel
Nous avons tout les deux préféré ce projet aux deux autres. Le projet nous a permis de réaliser une génération procédurale et surtout d'approfondir notre connaissance là dessus. Malgrè l'utilisation d'un tutoriel, en se posant dessus nous avons pu modifier le tutoriel pour arriver à ce qu'on voulait. Le fait que le projet était un projet de groupe étant aussi plus motivant pour travailler.

# Vidéo 

Vidéo 1 :https://www.youtube.com/watch?v=01l8KpWOtgc&feature=youtu.be&ab_channel=mimosa59112

Vidéo 2 :https://www.youtube.com/watch?v=SALrrI30wTw&feature=youtu.be&ab_channel=mimosa59112
