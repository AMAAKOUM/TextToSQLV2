# TextToSQLV2
Application Web pour Convertir un Texte vers une requête SQL.<br> 
Pour une mesure de sécurité on autoriser que les requêtes SELECT et de ne pas permettre les commandes de modification (INSERT, UPDATE, DELETE)
Une Option encours d'amelioration (Creation droits d'acces, et integration NLP)

<h2>Description & Architecture de l'application.</h2> 
Application suivra l'architecture MVC (Model-View-Controller), qui sépare les données et le traitements
pour rendre le code plus facile à gérer.

<h2>Model : Représente les données et la logique métier. </h2>
Ici, il s'agit des classes pour les requêtes SQL, les résultats, et les informations de la base de données. Il gère également l'interaction avec la base de données.

<h2>View : 	L'interface utilisateur. </h2>
C'est la page web que l'utilisateur voit, avec le champ de texte pour la saisie et le tableau pour l'affichage des résultats.

<h2> Controller : Gère les interactions de l'utilisateur.</h2>
Il reçoit la requête HTTP, demande au Model de faire le travail (traduire le texte, interroger la base de données), puis envoie les données à la View pour affichage.
