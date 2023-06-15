import 'package:flutter/material.dart';

import 'router/tab_navigator.dart';

enum BottomNavItem {
  one,
  two,
  three,
}

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  BottomNavItem selectedItem = BottomNavItem.one;

  final Map<BottomNavItem, GlobalKey<NavigatorState>> navigatorKeys = {
    BottomNavItem.one: GlobalKey<NavigatorState>(),
    BottomNavItem.two: GlobalKey<NavigatorState>(),
    BottomNavItem.three: GlobalKey<NavigatorState>(),
  };

  final Map<BottomNavItem, IconData> items = const {
    BottomNavItem.one: Icons.quiz_outlined,
    BottomNavItem.two: Icons.attach_money_outlined,
    BottomNavItem.three: Icons.connect_without_contact_outlined,
  };

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: WillPopScope(
        onWillPop: () async {
          // This is when you want to remove all the pages from the
          // stack for the specific BottomNav item.
          navigatorKeys[selectedItem]
              ?.currentState
              ?.popUntil((route) => route.isFirst);

          return false;
        },
        child: Stack(
          children: items
              .map(
                (item, _) => MapEntry(
                  item,
                  _buildOffstageNavigator(item, item == selectedItem),
                ),
              )
              .values
              .toList(),
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(
        backgroundColor: Colors.white,
        selectedItemColor: Theme.of(context).primaryColor,
        unselectedItemColor: Colors.grey,
        currentIndex: BottomNavItem.values.indexOf(selectedItem),
        showSelectedLabels: false,
        showUnselectedLabels: false,
        onTap: (index) {
          final currentSelectedItem = BottomNavItem.values[index];
          if (selectedItem == currentSelectedItem) {
            navigatorKeys[selectedItem]
                ?.currentState
                ?.popUntil((route) => route.isFirst);
          }
          setState(() {
            selectedItem = currentSelectedItem;
          });
        },
        items: items
            .map((item, icon) => MapEntry(
                item.toString(),
                BottomNavigationBarItem(
                    label: '',
                    icon: Icon(
                      icon,
                      size: 30.0,
                    ))))
            .values
            .toList(),
      ),
    );
  }

  Widget _buildOffstageNavigator(BottomNavItem currentItem, bool isSelected) {
    return Offstage(
      offstage: !isSelected,
      child: TabNavigator(
        navigatorKey: navigatorKeys[currentItem]!,
        item: currentItem,
      ),
    );
  }
}



/*
import 'package:flutter/material.dart';

import 'bot_page.dart';
import 'model/company_model.dart';
import 'reviews_page.dart';
import 'bonus_page.dart';

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  HomeState createState() => HomeState();
}

class HomeState extends State<Home> {
  int _selectedIndex = 0;

  static List<Widget> pages = <Widget>[
    const ReviewsPage(),
    const BonusPage(),
    const BotPage(),
  ];

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('ElSurv', style: Theme.of(context).textTheme.titleLarge),
      ),
      body: pages[_selectedIndex],
      bottomNavigationBar: BottomNavigationBar(
        selectedItemColor: Theme.of(context).textSelectionTheme.selectionColor,
        currentIndex: _selectedIndex,
        onTap: _onItemTapped,
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.card_giftcard),
            label: 'Reviews',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.card_giftcard),
            label: 'Bonus',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.card_giftcard),
            label: 'Bot',
          ),
        ],
      ),
    );
  }
}
*/