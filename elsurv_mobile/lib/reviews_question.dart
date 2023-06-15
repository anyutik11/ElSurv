import 'dart:developer';

import 'package:flutter/material.dart';

import 'environment.dart';
import 'model/review_model.dart';
import 'service/http_service.dart';

class QuestionPage extends StatelessWidget {
  //const ReviewsPage2({super.key});
  late String question;
  late int questionNum;
  late Review review;

  QuestionPage.fromQuestion(Key key, this.review, this.questionNum)
      : super(key: key) {
    log('review question: ${review.question1}');
  }

  @override
  Widget build(BuildContext context) {
    final myCustomForm = MyCustomForm(questNum: questionNum);
    return Center(
      child: Container(
        padding: const EdgeInsets.all(16),
        constraints: const BoxConstraints.expand(
          width: double.infinity,
          height: double.infinity,
        ),
        decoration: const BoxDecoration(
          image: DecorationImage(
            image: AssetImage('assets/mag1.png'),
            fit: BoxFit.cover,
          ),
          borderRadius: BorderRadius.all(Radius.circular(10.0)),
        ),
        child: TextButton(
            style: TextButton.styleFrom(
              textStyle: const TextStyle(fontSize: 20),
            ),
            onPressed: () => {},
            child: Center(
              child: ListView(shrinkWrap: true, children: <Widget>[
                /*
            ListTile(
              leading: const Icon(Icons.map),
              title: Text('question $questionNum'),
            ),
            */
                ListTile(
                  leading: const Icon(Icons.question_mark),
                  title: (questionNum == 1
                      ? Text(review.question1)
                      : questionNum == 2
                          ? Text(review.question2)
                          : Text(review.question3)),
                ),
                ListTile(
                  title: Center(child: myCustomForm),
                ),
                ListTile(
                  title: Center(
                      child: OutlinedButton(
                    style: OutlinedButton.styleFrom(
                      textStyle: const TextStyle(fontSize: 20),
                    ),
                    onPressed: () {
                      makeNavigation(context);
                    },
                    child: const Text('Next'),
                  )),
                ),
              ]),
            )),
      ),
    );
  }

  void makeNavigation(BuildContext context) {
    if (questionNum >= 3) {
      log('Answers: ${Params.answers[0]} ${Params.answers[1]} '
          '${Params.answers[2]}');

      postResult();

      Navigator.push(
          context,
          MaterialPageRoute(
            builder: (_) => Scaffold(
              appBar: AppBar(
                title: const Text('Thank you'),
              ),
              body: Center(
                  child: TextButton(
                style: TextButton.styleFrom(
                  textStyle: const TextStyle(fontSize: 20),
                ),
                onPressed: () {
                  Navigator.pop(context);
                  Navigator.pop(context);
                  Navigator.pop(context);
                  Navigator.pop(context);
                },
                child: const Center(
                    child: Text('Thank you very match. '
                        'Press to send resuts.')),
              )),
            ),
          ));
    } else {
      Navigator.push(
          context,
          MaterialPageRoute(
            builder: (_) => Scaffold(
              appBar: AppBar(
                title: const Text('Question 2'),
              ),
              body: QuestionPage.fromQuestion(
                  Key('key${questionNum + 4}'), review, questionNum + 1),
            ),
          ));
    }
  }
}

class MyCustomForm extends StatefulWidget {
  late int questNum = 0;
  MyCustomForm({super.key, required this.questNum});

  @override
  State<MyCustomForm> createState() => _MyCustomFormState(questNum);
}

// Define a correspondin
//g State class.
// This class holds the data related to the Form.
class _MyCustomFormState extends State<MyCustomForm> {
  // Create a text controller and use it to retrieve the current value
  // of the TextField.
  final myController = TextEditingController();
  late int questionNum;

  _MyCustomFormState(this.questionNum) : super();

  @override
  void initState() {
    super.initState();

    myController.addListener(() {
      setState(() {
        log('_txt: $myController.text');
        Params.answers[questionNum - 1] = myController.text;
      });
    });
  }

  @override
  void dispose() {
    //log('_MyCustomFormState dispose: ${myController.text}');
    //Params.answers.add(myController.text.toString());
    myController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: myController,
    );
  }
}
