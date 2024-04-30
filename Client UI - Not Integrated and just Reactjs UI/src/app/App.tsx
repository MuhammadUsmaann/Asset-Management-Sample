import { Suspense } from 'react'
import { Outlet } from 'react-router-dom'
import { I18nProvider } from '../_metronic/i18n/i18nProvider'
import { LayoutProvider, LayoutSplashScreen } from '../_metronic/layout/core'
import { MasterInit } from '../_metronic/layout/MasterInit'
import { AuthInit } from './modules/auth'
import { ThemeModeProvider } from '../_metronic/partials'
import { GoogleOAuthProvider } from '@react-oauth/google';

const App = () => {
  return (
    <Suspense fallback={<LayoutSplashScreen />}>
      <I18nProvider>
        <LayoutProvider>
          <ThemeModeProvider>
            <GoogleOAuthProvider clientId="617246850621-95f9qhmehd380g2df86pjhrqc84n8nij.apps.googleusercontent.com">
              <AuthInit>
                <Outlet />
                <MasterInit />
              </AuthInit>
            </GoogleOAuthProvider>
          </ThemeModeProvider>
        </LayoutProvider>
      </I18nProvider>
    </Suspense>
  )
}

export { App }
