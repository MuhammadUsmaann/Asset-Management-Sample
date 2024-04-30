import { FC, useState } from 'react'
import { useIntl } from 'react-intl'
import { PageTitle } from '../../../_metronic/layout/core'
import Table from '../../components/table'

const DashboardWrapper: FC = () => {
  const intl = useIntl()
  const [pageNo, setPageNo] = useState(1)
  return (
    <>
      <PageTitle breadcrumbs={[]}>{intl.formatMessage({ id: 'MENU.DASHBOARD' })}</PageTitle>

      <div className="p-10">
        <Table
          headers={['Product', 'Price', 'Deposit', 'Agent',]}
          pageNo={pageNo} setPageNo={setPageNo} totalPages={5}
          data={[
            ['HTML,', '$230', '$32', 'Barendly'],
            ['HTML,', '$230', '$32', 'Barendly'],
            ['HTML,', '$230', '$32', 'Barendly'],
            ['HTML,', '$230', '$32', 'Barendly'],
            ['HTML,', '$230', '$32', 'Barendly'],
          ]}
        />
      </div>
    </>
  )
}

export { DashboardWrapper }
