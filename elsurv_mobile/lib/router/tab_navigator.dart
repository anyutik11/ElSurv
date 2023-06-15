import 'package:flutter/material.dart';

import '../bonus_page.dart';
import '../bot_page.dart';
import '../home.dart';
import '../login_page.dart';
import '../reviews_page.dart';
import 'custom_router.dart';

class TabNavigator extends StatelessWidget {
  static const String tabNavigatorRoot = '/';

  final GlobalKey<NavigatorState> navigatorKey;
  final BottomNavItem item;

  const TabNavigator({Key? key, required this.navigatorKey, required this.item})
      : super(key: key);
  @override
  Widget build(BuildContext context) {
    final routeBuilders = _routeBuilder();
    return Navigator(
      key: navigatorKey,
      initialRoute: tabNavigatorRoot,
      onGenerateInitialRoutes: (_, initialRoute) {
        return [
          MaterialPageRoute(
              settings: const RouteSettings(name: tabNavigatorRoot),
              builder: (context) => routeBuilders[initialRoute]!(context))
        ];
      },
      onGenerateRoute: CustomRouter.onGenerateNestedRoute,
    );
  }

  Map<String, WidgetBuilder> _routeBuilder() {
    return {tabNavigatorRoot: (context) => _getScren(context, item)};
  }

  Widget _getScren(BuildContext context, BottomNavItem item) {
    switch (item) {
      case BottomNavItem.one:
        return const ReviewsPage();
      case BottomNavItem.two:
        //return const BonusPage();
        return LoginPage();
      case BottomNavItem.three:
        return const BotPage();
      default:
        return const Scaffold();
    }
  }
}
