A lancer depuis un des trois monde (Monde1, Monde 4, Monde 5)

Ce projet n'est pas parfait mais je n'avais malheureusement pas plus de 10 heures a lui consacrer a cause de la fin du semestre. Voici une description des choix que j'ai fait, des difficultes que j'ai eues, et comment a ete decoupe l'utilisation de mon temps.

1ere partie: 3 heures
- installation de unity et VsCode
- developpement du controller de type Walking Simulator pour 1 joueur qui peut se deplacer dans l'espace, attraper les cubes, les mettre en mode focus, et se deplacer dans 3 mondes differents a travers des portals
- j'ai decide d'utiliser le nouveau system de input de unity pour pouvoir facilement declencher des evenements avec le clavier et une potentielle manette

Problemes rencontres:
- lorsque le player changeait de monde, il spawnait toujours a l'origine puisque c'est la que le player etait place dans la scene. J'aurais bien voulu qu'il spawn devant le portal du monde d'ou il venait pour que ca soit plus fluide
donc j'ai cree un script qui le deplaceait lors de sa fonction awake a la position correspondante. Cependant le SceneManager ne garde pas un historique des scenes ouvertes donc j'ai du le coder dans un script appele PlayerPlacer qui gardait un historique a la main. 

2eme partie: 7 heures 
- implementation du monde multijoueur
- je n'avais jamais rien fait en multijoueur donc j'ai du lire un peu l'etat de l'art et j'ai coisi d'utiliser Mirror
- j'ai decide d'utiliser ParrelSync pour tester les fonctions multijoueurs
- l'implemantation finale est la meme que celle avec 1 joeur mais en multijoueur (les players se deplacent dans 3 monde en pouvant attraper et lacher les cubes et les mettre en mode focus)

Problemes rencontres:
- Une fois que j'arrivais a lancer 2 sessions avec 2 joueurs, j'avais en fait 2 jeux independants dans ou dans chacun le deuxieme player etait immobile.
- Ensuite, une fois que les mouvements etaient synchronises entre les 2 sessions, quand je bougeais un joueur, le deuxieme bougeait exactement pareil. J'ai donc du desactiver dans chaque session les scripts qui gerent le mouvement du player de tous les players sauf le player local
- Ensuite, seulement mon player qui etait dans la session host du server pouvait interagir avec les objects. J'ai donc du ecrire un script qui, a chaque fois qu'un joueur voulait interagir avec un cube, retirait l'authorite sur ce cube au serveur et la donnait au client sur lequel se trouvait le joueur voulant interagir.
- Finalement un gros probleme a ete le changement de scene puisque quand je changeais de scene, le NetworkManager ne spawnait pas le playerPrefab que je lui avait renseigne. Apres une longue lecture de la documentation Mirror, je me suis rendue compte que j'utilisais une fonction de changement de scene d'une version anterieure qui avait ete changee depuis et qu'il suffisait de renommer.
  
