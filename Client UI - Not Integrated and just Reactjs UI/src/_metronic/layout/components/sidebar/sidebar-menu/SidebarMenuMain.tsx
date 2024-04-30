/* eslint-disable @typescript-eslint/no-explicit-any */
// import { useIntl } from 'react-intl'
import { SidebarMenuItem } from './SidebarMenuItem'
// import { SidebarMenuItemWithSub } from './SidebarMenuItemWithSub'

const SidebarMenuMain = () => {
  // const intl = useIntl()

  return (
    <>
      {/* <SidebarMenuItem
        to='/dashboard'
        icon='element-11'
        title={intl.formatMessage({ id: 'MENU.DASHBOARD' })}
        fontIcon='bi-app-indicator'
      /> */}
      <SidebarMenuItem
        to='/user-management'
        icon='abstract-28'
        title='User management'
        fontIcon='bi-layers'
      />
      {/* <SidebarMenuItemWithSub
        to='/profile/overview'
        title='Profile'
        icon='profile-circle'
        fontIcon='bi-person'
      >
        <SidebarMenuItem to='/profile/overview' title='Overview' hasBullet={true} />
        <SidebarMenuItem to='/profile/settings' title='Setting' hasBullet={true} />
      </SidebarMenuItemWithSub> */}
    </>
  )
}

export { SidebarMenuMain }
