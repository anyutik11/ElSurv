import 'dart:developer';

import 'package:flutter/material.dart';

import 'environment.dart';
import 'model/review_model.dart';
import 'reviews_question.dart';
import 'service/http_service.dart';

class ReviewsPage2 extends StatefulWidget {
  //const ReviewsPage2({super.key});
  late String companyId;

  ReviewsPage2.fromCompany(Key key, String compId) : super(key: key) {
    companyId = compId;
  }

  @override
  State<ReviewsPage2> createState() =>
      _ReviewsPage2State.fromCompany(companyId);
}

class _ReviewsPage2State extends State<ReviewsPage2> {
  late Future<List<Review>> futureReviews;
  late String companyId;

  _ReviewsPage2State.fromCompany(String compId) : super() {
    companyId = compId;
    log('feeth reviews for: ${companyId}');
  }

  @override
  void initState() {
    super.initState();
    futureReviews = fetchReviews(companyId);
  }

  @override
  Widget build(BuildContext context) {
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
        child: FutureBuilder<List<Review>>(
          future: futureReviews,
          builder: (context, snapshot) {
            if (snapshot.hasData) {
              return listOfReviews(context, snapshot);
            } else if (snapshot.hasError) {
              return Text('${snapshot.error}');
            }

            return const CircularProgressIndicator();
          },
        ),
      ),
    );
  }

  Widget listOfReviews(
      BuildContext context, AsyncSnapshot<List<Review>> snapshot) {
    //return Text(snapshot.data!.shortName);
    int i = 1;
    return DataTable(
      showCheckboxColumn: false,
      columns: const <DataColumn>[
        DataColumn(
          label: Expanded(
            child: Text(
              '#',
              style: TextStyle(fontStyle: FontStyle.italic),
            ),
          ),
        ),
        DataColumn(
          label: Expanded(
            child: Text(
              'Descr',
              style: TextStyle(fontStyle: FontStyle.italic),
            ),
          ),
        ),
      ],
      rows: snapshot.data
              ?.map((review) => DataRow(
                    onSelectChanged: (bool? selected) {
                      if (selected == true) {
                        log('review-selected: ${review.id}');
                        Params.reviewId = review.id;
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                              //builder: (context) => const ReviewsPage2()));
                              builder: (_) => Scaffold(
                                appBar: AppBar(
                                  title: const Text('Questions 1'),
                                ),
                                body: QuestionPage.fromQuestion(
                                    const Key('key04'), review, 1),
                              ),
                            ));
                      }
                    },
                    cells: <DataCell>[
                      DataCell(Text((i++).toString())),
                      DataCell(Text(review.description))
                    ],
                  ))
              .toList() ??
          [],
    );
  }
}
