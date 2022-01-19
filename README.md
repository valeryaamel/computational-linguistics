# Задание  
Вариант 1.  
Студенты объединяются в группы по 3 человека. Состав групп может быть определен студентами самостоятельно, либо назначается преподавателем. Варианты заданий раздаются преподавателем. Разработка осуществляется на языке python в ОС Linux.  
Вместе с программным кодом разрабатывается Пояснительная записка, содержащая описание алгоритмов, методов и стороннего использованного ПО.  
#Сайт-парсер - http://www.v1parser.somee.com/  
____
# Задание для первого студента
Распарсить сайт https://v1.ru/ и вывести в web-интерфейсе данные согласно номеру задания. Краулер должен считывать новостную ленту с первой страницы сайта. Периодичность повторения устанавливается пользователем. Данные заполняются в БД MongoDB. Обязательные поля для текста новости:  
* Название новости  
* Дата новости  
* Ссылка на новость  
* Текст новости  
* Количество просмотров новости (если есть)  
* Количество комментариев новости (если есть)  
____
# Задание для второго студента  
Создать программный модкль для анализа новостей из БД. Выделить с помощью Томита-парсера упоминание в тексте значимых персон Волгоградской области и достопримечательностей. Зафиксировать в БД предложения с их упоминанием для дальнейшего анализа тональности.  
Создать программный модуль для проведения с помощью Spark MlLib анализ модели word2vec на всем объеме новостных статей из БД. Для персон Волгоградской области и достопримечательностей определить контектные синонимы и слова, с которыми они упоминались в тексте.  
Персоны https://global-volgograd.ru/person  
Достопримечательности https://avolgograd.com/sights?obl=vgg  
____
# Задание для третьего студента
Создать программный модуль для выявления тональности высказываний по отношению к персонам Волгоградской области и достопримечательностям.  
Можно использовать либо подход на основе правил и словарей, либо методы машинного обучения.  
____
Студенты группы ИВТ-363
- Авилов В.С.
- Беляк Н.Д.
- Мельникова В.И.
