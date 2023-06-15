import 'dart:developer';

import 'package:flutter/material.dart';

import 'environment.dart';
import 'model/bonuce_model.dart';
import 'profile_card.dart';
import 'main_theme.dart';
import 'service/http_service.dart';

class BonusPage extends StatelessWidget {
  const BonusPage({super.key});
  @override
  Widget build(BuildContext context) {
    return Center(
      child: Container(
        constraints: const BoxConstraints.expand(
          width: double.infinity,
          height: double.infinity,
        ),
        margin: const EdgeInsets.only(top: 60),
        decoration: const BoxDecoration(
          image: DecorationImage(
            image: AssetImage('assets/mag4.png'),
            fit: BoxFit.cover,
          ),
          borderRadius: BorderRadius.all(
            Radius.circular(10.0),
          ),
        ),
        child: Column(
          children: [
            AuthorCard(
              guestName: Params.guest?.name ?? '',
              title: 'Best our guest',
              //imageProvider: AssetImage('assets/avatar.png'),
            ),
            const BonuceListPage(),
            Expanded(
              child: Stack(
                children: [
                  Positioned(
                    bottom: 16,
                    right: 16,
                    child: Text(
                      'Bonus',
                      style: ElSurvTheme.lightTextTheme.displayLarge,
                    ),
                  ),
                  Positioned(
                    bottom: 70,
                    left: 16,
                    child: RotatedBox(
                      quarterTurns: 3,
                      child: Text(
                        'Welcom to our best restaurants',
                        style: ElSurvTheme.lightTextTheme.displayLarge,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class BonuceListPage extends StatefulWidget {
  const BonuceListPage({super.key});

  @override
  State<BonuceListPage> createState() => _BonuceListPageState();
}

class _BonuceListPageState extends State<BonuceListPage> {
  late Future<List<Bonuce>> futureBonuces;

  @override
  void initState() {
    log('_BonuceListPageState.initState');
    super.initState();
    futureBonuces = fetchBonuces('guest-id-01');
  }

  @override
  Widget build(BuildContext context) {
    //log('data: ' + Params.host);
    //debugPrint

    return Center(
      child: FutureBuilder<List<Bonuce>>(
        future: futureBonuces,
        builder: (context, snapshot) {
          if (snapshot.hasData) {
            return listOfBonuces(context, snapshot);
          } else if (snapshot.hasError) {
            return Text('${snapshot.error}');
          }

          return const CircularProgressIndicator();
        },
      ),
    );
  }

  Widget listOfBonuces(
      BuildContext context, AsyncSnapshot<List<Bonuce>> snapshot) {
    return DataTable(columns: const <DataColumn>[
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
            'Date',
            style: TextStyle(fontStyle: FontStyle.italic),
          ),
        ),
      ),
      DataColumn(
        label: Expanded(
          child: Text(
            'Bonuce',
            style: TextStyle(fontStyle: FontStyle.italic),
          ),
        ),
      ),
      DataColumn(
        label: Expanded(
          child: Text(
            'Description',
            style: TextStyle(fontStyle: FontStyle.italic),
          ),
        ),
      ),
    ], rows: tblRows(snapshot));
  }

  List<DataRow> tblRows(AsyncSnapshot<List<Bonuce>> snapshot) {
    int i = 1;
    int bonuce = 0;
    final res = snapshot.data?.map((comp) {
          bonuce += comp.sum ?? 0;
          return DataRow(cells: <DataCell>[
            DataCell(Text((i++).toString())),
            DataCell(Text(comp.dtd)),
            DataCell(Text(comp.sum?.toString() ?? '')),
            DataCell(Text(comp.remark)),
          ]);
        }).toList() ??
        [];

    res.add(DataRow(cells: <DataCell>[
      const DataCell(Text((''))),
      const DataCell(Text('Total:')),
      DataCell(Text(bonuce.toString())),
      const DataCell(Text('')),
    ]));

    return res;
  }
}
