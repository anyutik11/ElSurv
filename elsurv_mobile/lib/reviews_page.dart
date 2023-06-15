import 'dart:developer';

import 'package:flutter/material.dart';

import 'environment.dart';
import 'model/company_model.dart';
import 'reviews_page2.dart';
import 'service/http_service.dart';

class ReviewsPage extends StatefulWidget {
  const ReviewsPage({super.key});

  @override
  State<ReviewsPage> createState() => _ReviewsPageState();
}

class _ReviewsPageState extends State<ReviewsPage> {
  late Future<List<Company>> futureCompanies;

  @override
  void initState() {
    log('_ReviewsPageState.initState params.length: ${Params.answers.length}');
    super.initState();
    futureCompanies = fetchCompanies();
  }

  @override
  Widget build(BuildContext context) {
    //log('data: ' + Params.host);
    //debugPrint

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
        child: FutureBuilder<List<Company>>(
          future: futureCompanies,
          builder: (context, snapshot) {
            if (snapshot.hasData) {
              return listOfCompanies(context, snapshot);
            } else if (snapshot.hasError) {
              return Text('${snapshot.error}');
            }

            return const CircularProgressIndicator();
          },
        ),
      ),
    );
  }

  Widget listOfCompanies(
      BuildContext context, AsyncSnapshot<List<Company>> snapshot) {
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
              'Company',
              style: TextStyle(fontStyle: FontStyle.italic),
            ),
          ),
        ),
      ],
      /*
      rows: <DataRow>[
        DataRow(
          cells: <DataCell>[
            DataCell(Text(i.toString())),
            DataCell(Text(snapshot.data!.shortName)),
            DataCell(Text('xxx')),
          ],
        ),
      ],
      */
      rows: snapshot.data
              ?.map((comp) => DataRow(
                    onSelectChanged: (bool? selected) {
                      if (selected == true) {
                        log('row-selected: ${comp.id}');
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                              //builder: (context) => const ReviewsPage2()));
                              builder: (_) => Scaffold(
                                appBar: AppBar(
                                  title: const Text('List of reviews'),
                                ),
                                body: ReviewsPage2.fromCompany(
                                    const Key('key01'), comp.id),
                              ),
                            ));
                      }
                    },
                    cells: [
                      DataCell(Text((i++).toString())),
                      DataCell(Text(comp.fullName))
                    ],
                  ))
              .toList() ??
          [],
    );
  }
}
