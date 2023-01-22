# 🪙YsrisSignal 
## Récupération et stockage des cours des crypto-monnaies
### Un exemple de récupération des cours des crypto-monnaies en utilisant l'API Binance et en les stockant dans une base de données SQL Server. 
### Nous utilisons également des bibliothèques telles que Newtonsoft.Json et Dapper pour faciliter le développement de l'application. 
### Nous implémentons le design pattern Visitor pour améliorer la qualité du code.

L'analyse des cours des crypto-monnaies est un élément crucial pour les traders et les investisseurs dans ce marché en constante évolution. Il est donc primordial d'avoir un accès en temps réel aux prix des différentes crypto-monnaies. C'est pourquoi, dans cet article, nous allons vous présenter une méthode pour récupérer les cours de différentes crypto-monnaies comme le Bitcoin (BTC), Monero (XMR) et l'Euro (EUR) contre le Dollar américain (USDT), à des intervalles d'une heure et quatre heures, en utilisant le langage de programmation C#.

La récupération des cours s'effectue grâce à l'utilisation de l'API Binance, l'une des principales plateformes de trading de crypto-monnaies, et de la bibliothèque Newtonsoft.Json pour traiter les données JSON. Pour stocker les données récupérées, nous utilisons Dapper, une bibliothèque permettant d'accéder à une base de données SQL Server.

Pour commencer, nous créons une classe TimeSerieItem qui permet de stocker les informations relatives aux cours, comme le symbole, l'intervalle, le prix et la date. Cette classe possède également des propriétés pour stocker les informations relatives à l'indicateur Ichimoku. Par la suite, nous créons une classe BinanceService contenant une méthode GetPrice pour récupérer les cours à partir de l'API Binance.

Afin de stocker les cours récupérés, nous créons une classe TimeSerieItemDal qui permet de stocker les données dans la base de données. Cette classe utilise Dapper pour gérer les accès à la base de données. Nous avons également implémenté la comparaison entre les TimeSerieItem pour éviter de stocker deux fois les mêmes cours dans la base de données.

Pour améliorer la qualité du code, nous avons également implémenté le design pattern Visitor. Ce dernier permet de séparer les responsabilités en créant une classe visiteur pour chaque action à effectuer sur les données récupérées. Ainsi, il devient possible d'ajouter de nouvelles fonctionnalités sans altérer le fonctionnement de l'application.
